//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace CalculatorShell.Engine.Simplification;

internal static class QuineMcclusky
{
    private static Dictionary<int, List<Implicant>> Group(List<Implicant> implicants)
    {
        Dictionary<int, List<Implicant>>? group = new();
        foreach (Implicant m in implicants)
        {
            int oneCount = m.Mask.Count(c => c == '1');

            if (!group.ContainsKey(oneCount))
                group.Add(oneCount, new List<Implicant>());

            group[oneCount].Add(m);
        }

        return group;
    }

    private static bool Simplify(ref List<Implicant> implicants)
    {
        //Group by number of 1's and determine relationships by comparing.
        Dictionary<int, List<Implicant>>? groups = Group(implicants).OrderBy(i => i.Key).ToDictionary(i => i.Key, i => i.Value);

        List<ImplicantRelationship>? relationships = new();
        for (int i = 0; i < groups.Keys.Count; i++)
        {
            if (i == (groups.Keys.Count - 1)) break;

            IEnumerable<ImplicantRelationship>? q = from a in groups[groups.Keys.ElementAt(i)]
                                                    from b in groups[groups.Keys.ElementAt(i + 1)]
                                                    where Utilities.GetDifferences(a.Mask, b.Mask) == 1
                                                    select new ImplicantRelationship(a, b);

            relationships.AddRange(q);
        }

        /*
         * For each relationship, find the affected minterms and remove them.
         * Then add a new implicant which simplifies the affected minterms.
         */
        foreach (ImplicantRelationship r in relationships)
        {
            List<Implicant>? rmList = new();

            foreach (Implicant m in implicants)
            {
                if (r.A.Equals(m) || r.B.Equals(m)) rmList.Add(m);
            }

            foreach (Implicant m in rmList) _ = implicants.Remove(m);

            var mask = Utilities.GetMask(r.A.Mask, r.B.Mask);

            Implicant? newImplicant = new(mask, r.A.Minterms.Concat(r.B.Minterms));

            bool exist = implicants.Any(m => m.Mask == newImplicant.Mask);

            if (!exist)
                implicants.Add(newImplicant);
        }

        //Return true if simplification occurred, false otherwise.
        return relationships.Count != 0;
    }

    private static void PopulateMatrix(ref bool[,] matrix, List<Implicant> implicants, List<int> inputs)
    {
        for (int m = 0; m < implicants.Count; m++)
        {
            int y = implicants.IndexOf(implicants[m]);

            foreach (int i in implicants[m].Minterms)
            {
                for (int index = 0; index < inputs.Count; index++)
                {
                    if (i == inputs[index])
                        matrix[y, index] = true;
                }
            }
        }
    }

    private static List<Implicant> SelectImplicants(List<Implicant> implicants, List<int> inputs)
    {
        List<int>? lstToRemove = new(inputs);
        List<Implicant>? final = new();
        int runnumber = 0;
        while (lstToRemove.Count != 0)
        {
            foreach (Implicant? m in implicants)
            {
                bool add = false;

                if (Utilities.ContainsSubList(lstToRemove, m.Minterms))
                {
                    add = true;
                    if (lstToRemove.Count < m.Minterms.Count) break;
                }
                else add = false;

                if ((((lstToRemove.Count <= m.Minterms.Count) && !add) || runnumber > 5)
                    && Utilities.ContainsAtleastOne(lstToRemove, m.Minterms)
                    && runnumber > 5)
                {
                    add = true;
                }

                if (add)
                {
                    final.Add(m);
                    foreach (int r in m.Minterms) _ = lstToRemove.Remove(r);
                }
            }
            foreach (Implicant? item in final) _ = implicants.Remove(item); //ami benne van már 1x, az még 1x ne
            ++runnumber;
        }

        return final;
    }

    private static string GetFinalExpression(List<Implicant> implicants, string[] variables)
    {
        int longest = 0;
        StringBuilder final = new();

        if (implicants.Any())
            longest = implicants.Max(m => m.Mask.Length);

        for (int i = implicants.Count - 1; i >= 0; i--)
        {
            string s = ImplicantStringFactory.Create(implicants[i], longest, variables);
            _ = final.Append(s);
            _ = final.Append(" | ");
        }

        string ret = final.Length > 3 ? final.ToString()[0..^3] : final.ToString();
        return ret switch
        {
            "" => "false",
            " | " => "true",
            " & " => "false",
            _ => ret,
        };
    }

    public static string GetSimplified(IEnumerable<int> care, string[] variables)
    {
        List<Implicant> implicants = new();
        List<int> all = care.Order().Distinct().ToList();

        foreach (int item in all)
        {
            Implicant? m = new()
            {
                Mask = Utilities.GetBinaryValue(item, variables.Length),
            };
            _ = m.Minterms.Add(item);
            implicants.Add(m);
        }

        while (Simplify(ref implicants))
        {
            //Populate a matrix.
            bool[,] matrix = new bool[implicants.Count, all.Count]; //x, y
            PopulateMatrix(ref matrix, implicants, all);
        }

        return GetFinalExpression(SelectImplicants(implicants, care.ToList()), variables);
    }
}
