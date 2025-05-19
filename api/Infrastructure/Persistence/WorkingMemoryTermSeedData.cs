using Domain.Entities.WorkingMemoryAggregate;

namespace Infrastructure.Persistence;

public class WorkingMemoryTermSeedData
{
    public static List<WorkingMemoryTerm> GetOneBackTerms()
    {
        var oneBackTerms = new List<WorkingMemoryTerm>();

        oneBackTerms.Add(new WorkingMemoryTerm(1, false, 1, 1));
        oneBackTerms.Add(new WorkingMemoryTerm(2, false, 1, 2));
        oneBackTerms.Add(new WorkingMemoryTerm(3, true, 1, 2));
        oneBackTerms.Add(new WorkingMemoryTerm(4, false, 1, 3));
        oneBackTerms.Add(new WorkingMemoryTerm(5, false, 1, 4));
        oneBackTerms.Add(new WorkingMemoryTerm(6, false, 1, 5));
        oneBackTerms.Add(new WorkingMemoryTerm(7, true, 1, 5));
        oneBackTerms.Add(new WorkingMemoryTerm(8, false, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(9, false, 1, 7));
        oneBackTerms.Add(new WorkingMemoryTerm(10, true, 1, 7));
        oneBackTerms.Add(new WorkingMemoryTerm(11, false, 1, 8));
        oneBackTerms.Add(new WorkingMemoryTerm(12, false, 1, 9));
        oneBackTerms.Add(new WorkingMemoryTerm(13, false, 1, 10));
        oneBackTerms.Add(new WorkingMemoryTerm(14, false, 1, 11));
        oneBackTerms.Add(new WorkingMemoryTerm(15, true, 1, 11));

        return oneBackTerms;
    }


    public static List<WorkingMemoryTerm> GetTwoBackTerms()
    {
        var twoBackTerms = new List<WorkingMemoryTerm>();

        twoBackTerms.Add(new WorkingMemoryTerm(1, false, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(2, false, 2, 2));
        twoBackTerms.Add(new WorkingMemoryTerm(3, true, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(4, false, 2, 3));
        twoBackTerms.Add(new WorkingMemoryTerm(5, false, 2, 4));
        twoBackTerms.Add(new WorkingMemoryTerm(6, true, 2, 3));
        twoBackTerms.Add(new WorkingMemoryTerm(7, false, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(8, false, 2, 6));
        twoBackTerms.Add(new WorkingMemoryTerm(9, true, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(10, true, 2, 6));
        twoBackTerms.Add(new WorkingMemoryTerm(11, false, 2, 7));
        twoBackTerms.Add(new WorkingMemoryTerm(12, false, 2, 7));
        twoBackTerms.Add(new WorkingMemoryTerm(13, false, 2, 8));
        twoBackTerms.Add(new WorkingMemoryTerm(14, false, 2, 9));
        twoBackTerms.Add(new WorkingMemoryTerm(15, true, 2, 8));

        return twoBackTerms;
    }
    
    public static List<WorkingMemoryTerm> GetThreeBackTerms()
    {
        var threeBackTerms = new List<WorkingMemoryTerm>();

        threeBackTerms.Add(new WorkingMemoryTerm(1, false, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(2, false, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(3, false, 3, 2));
        threeBackTerms.Add(new WorkingMemoryTerm(4, false, 3, 3));
        threeBackTerms.Add(new WorkingMemoryTerm(5, true, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(6, false, 3, 3));
        threeBackTerms.Add(new WorkingMemoryTerm(7, false, 3, 4));
        threeBackTerms.Add(new WorkingMemoryTerm(8, false, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(9, true, 3, 3));
        threeBackTerms.Add(new WorkingMemoryTerm(10, false, 3, 6));
        threeBackTerms.Add(new WorkingMemoryTerm(11, true, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(12, false, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(13, false, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(14, false, 3, 7));
        threeBackTerms.Add(new WorkingMemoryTerm(15, true, 3, 5));

        return threeBackTerms;
    }
}
