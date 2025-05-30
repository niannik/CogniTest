using Domain.Entities.WorkingMemoryAggregate;

namespace Infrastructure.Persistence;

public class WorkingMemoryTermSeedData
{
    public static List<WorkingMemoryTerm> GetOneBackTerms()
    {
        var oneBackTerms = new List<WorkingMemoryTerm>();

        #region Block 1
        oneBackTerms.Add(new WorkingMemoryTerm(1, false, 1, 1, 14));
        oneBackTerms.Add(new WorkingMemoryTerm(2, false, 1, 1, 3));
        oneBackTerms.Add(new WorkingMemoryTerm(3, false, 1, 1, 2));
        oneBackTerms.Add(new WorkingMemoryTerm(4, true, 1, 1, 2));
        oneBackTerms.Add(new WorkingMemoryTerm(5, false, 1, 1, 11));
        oneBackTerms.Add(new WorkingMemoryTerm(6, true, 1, 1, 11));
        oneBackTerms.Add(new WorkingMemoryTerm(7, false, 1, 1, 5));
        oneBackTerms.Add(new WorkingMemoryTerm(8, false, 1, 1, 4));
        oneBackTerms.Add(new WorkingMemoryTerm(9, true, 1, 1, 4));
        oneBackTerms.Add(new WorkingMemoryTerm(10, false, 1, 1, 10));
        oneBackTerms.Add(new WorkingMemoryTerm(11, false, 1, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(12, false, 1, 1, 1));
        oneBackTerms.Add(new WorkingMemoryTerm(13, true, 1, 1, 1));
        oneBackTerms.Add(new WorkingMemoryTerm(14, false, 1, 1, 13));
        oneBackTerms.Add(new WorkingMemoryTerm(15, true, 1, 1, 13));
        oneBackTerms.Add(new WorkingMemoryTerm(16, false, 1, 1, 12));
        oneBackTerms.Add(new WorkingMemoryTerm(17, false, 1, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(18, false, 1, 1, 7));
        oneBackTerms.Add(new WorkingMemoryTerm(19, false, 1, 1, 9));
        oneBackTerms.Add(new WorkingMemoryTerm(20, true, 1, 1, 9));
        #endregion

        #region Block 2
        oneBackTerms.Add(new WorkingMemoryTerm(21, false, 2, 1, 2));
        oneBackTerms.Add(new WorkingMemoryTerm(22, false, 2, 1, 14));
        oneBackTerms.Add(new WorkingMemoryTerm(23, false, 2, 1, 12));
        oneBackTerms.Add(new WorkingMemoryTerm(24, true, 2, 1, 12));
        oneBackTerms.Add(new WorkingMemoryTerm(25, false, 2, 1, 4));
        oneBackTerms.Add(new WorkingMemoryTerm(26, true, 2, 1, 4));
        oneBackTerms.Add(new WorkingMemoryTerm(27, false, 2, 1, 3));
        oneBackTerms.Add(new WorkingMemoryTerm(28, true, 2, 1, 3));
        oneBackTerms.Add(new WorkingMemoryTerm(29, false, 2, 1, 1));
        oneBackTerms.Add(new WorkingMemoryTerm(30, false, 2, 1, 13));
        oneBackTerms.Add(new WorkingMemoryTerm(31, true, 2, 1, 13));
        oneBackTerms.Add(new WorkingMemoryTerm(32, false, 2, 1, 10));
        oneBackTerms.Add(new WorkingMemoryTerm(33, false, 2, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(34, true, 2, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(35, false, 2, 1, 11));
        oneBackTerms.Add(new WorkingMemoryTerm(36, false, 2, 1, 9));
        oneBackTerms.Add(new WorkingMemoryTerm(37, false, 2, 1, 6));
        oneBackTerms.Add(new WorkingMemoryTerm(38, false, 2, 1, 7));
        oneBackTerms.Add(new WorkingMemoryTerm(39, true, 2, 1, 7));
        oneBackTerms.Add(new WorkingMemoryTerm(40, false, 2, 1, 5));
        #endregion

        return oneBackTerms;
    }

    public static List<WorkingMemoryTerm> GetTwoBackTerms()
    {
        var twoBackTerms = new List<WorkingMemoryTerm>();

        #region Block 1
        twoBackTerms.Add(new WorkingMemoryTerm(1, false, 1, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(2, false, 1, 2, 9));
        twoBackTerms.Add(new WorkingMemoryTerm(3, false, 1, 2, 8));
        twoBackTerms.Add(new WorkingMemoryTerm(4, false, 1, 2, 11));
        twoBackTerms.Add(new WorkingMemoryTerm(5, true, 1, 2, 8));
        twoBackTerms.Add(new WorkingMemoryTerm(6, false, 1, 2, 10));
        twoBackTerms.Add(new WorkingMemoryTerm(7, false, 1, 2, 14));
        twoBackTerms.Add(new WorkingMemoryTerm(8, true, 1, 2, 10));
        twoBackTerms.Add(new WorkingMemoryTerm(9, false, 1, 2, 4));
        twoBackTerms.Add(new WorkingMemoryTerm(10, false, 1, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(11, true, 1, 2, 4));
        twoBackTerms.Add(new WorkingMemoryTerm(12, true, 1, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(13, false, 1, 2, 12));
        twoBackTerms.Add(new WorkingMemoryTerm(14, false, 1, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(15, false, 1, 2, 13));
        twoBackTerms.Add(new WorkingMemoryTerm(16, true, 1, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(17, false, 1, 2, 6));
        twoBackTerms.Add(new WorkingMemoryTerm(18, false, 1, 2, 7));
        twoBackTerms.Add(new WorkingMemoryTerm(19, false, 1, 2, 3));
        twoBackTerms.Add(new WorkingMemoryTerm(20, true, 1, 2, 7));
        #endregion

        #region Block 2
        twoBackTerms.Add(new WorkingMemoryTerm(21, false, 2, 2, 13));
        twoBackTerms.Add(new WorkingMemoryTerm(22, false, 2, 2, 6));
        twoBackTerms.Add(new WorkingMemoryTerm(23, false, 2, 2, 2));
        twoBackTerms.Add(new WorkingMemoryTerm(24, false, 2, 2, 11));
        twoBackTerms.Add(new WorkingMemoryTerm(25, true, 2, 2, 2));
        twoBackTerms.Add(new WorkingMemoryTerm(26, false, 2, 2, 7));
        twoBackTerms.Add(new WorkingMemoryTerm(27, false, 2, 2, 14));
        twoBackTerms.Add(new WorkingMemoryTerm(28, true, 2, 2, 7));
        twoBackTerms.Add(new WorkingMemoryTerm(29, false, 2, 2, 12));
        twoBackTerms.Add(new WorkingMemoryTerm(30, false, 2, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(31, false, 2, 2, 11));
        twoBackTerms.Add(new WorkingMemoryTerm(32, true, 2, 2, 5));
        twoBackTerms.Add(new WorkingMemoryTerm(33, false, 2, 2, 4));
        twoBackTerms.Add(new WorkingMemoryTerm(34, false, 2, 2, 9));
        twoBackTerms.Add(new WorkingMemoryTerm(35, true, 2, 2, 4));
        twoBackTerms.Add(new WorkingMemoryTerm(36, false, 2, 2, 10));
        twoBackTerms.Add(new WorkingMemoryTerm(37, false, 2, 2, 8));
        twoBackTerms.Add(new WorkingMemoryTerm(38, true, 2, 2, 10));
        twoBackTerms.Add(new WorkingMemoryTerm(39, false, 2, 2, 1));
        twoBackTerms.Add(new WorkingMemoryTerm(40, true, 2, 2, 10));
        #endregion

        return twoBackTerms;
    }
    
    public static List<WorkingMemoryTerm> GetThreeBackTerms()
    {
        var threeBackTerms = new List<WorkingMemoryTerm>();

        #region Block 1
        threeBackTerms.Add(new WorkingMemoryTerm(1, false, 1, 3, 13));
        threeBackTerms.Add(new WorkingMemoryTerm(2, false, 1, 3, 2));
        threeBackTerms.Add(new WorkingMemoryTerm(3, false, 1, 3, 14));
        threeBackTerms.Add(new WorkingMemoryTerm(4, true, 1, 3, 13));
        threeBackTerms.Add(new WorkingMemoryTerm(5, false, 1, 3, 3));
        threeBackTerms.Add(new WorkingMemoryTerm(6, true, 1, 3, 14));
        threeBackTerms.Add(new WorkingMemoryTerm(7, false, 1, 3, 12));
        threeBackTerms.Add(new WorkingMemoryTerm(8, true, 1, 3, 3));
        threeBackTerms.Add(new WorkingMemoryTerm(9, false, 1, 3, 6));
        threeBackTerms.Add(new WorkingMemoryTerm(10, true, 1, 3, 12));
        threeBackTerms.Add(new WorkingMemoryTerm(11, false, 1, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(12, false, 1, 3, 10));
        threeBackTerms.Add(new WorkingMemoryTerm(13, false, 1, 3, 4));
        threeBackTerms.Add(new WorkingMemoryTerm(14, true, 1, 3, 5));
        threeBackTerms.Add(new WorkingMemoryTerm(15, false, 1, 3, 8));
        threeBackTerms.Add(new WorkingMemoryTerm(16, false, 1, 3, 7));
        threeBackTerms.Add(new WorkingMemoryTerm(17, false, 1, 3, 9));
        threeBackTerms.Add(new WorkingMemoryTerm(18, false, 1, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(19, false, 1, 3, 10));
        threeBackTerms.Add(new WorkingMemoryTerm(20, true, 1, 3, 9));
        #endregion

        #region Block 2
        threeBackTerms.Add(new WorkingMemoryTerm(21, false, 2, 3, 8));
        threeBackTerms.Add(new WorkingMemoryTerm(22, false, 2, 3, 4));
        threeBackTerms.Add(new WorkingMemoryTerm(23, false, 2, 3, 2));
        threeBackTerms.Add(new WorkingMemoryTerm(24, true, 2, 3, 8));
        threeBackTerms.Add(new WorkingMemoryTerm(25, false, 2, 3, 6));
        threeBackTerms.Add(new WorkingMemoryTerm(26, true, 2, 3, 2));
        threeBackTerms.Add(new WorkingMemoryTerm(27, false, 2, 3, 12));
        threeBackTerms.Add(new WorkingMemoryTerm(28, true, 2, 3, 6));
        threeBackTerms.Add(new WorkingMemoryTerm(29, false, 2, 3, 7));
        threeBackTerms.Add(new WorkingMemoryTerm(30, false, 2, 3, 10));
        threeBackTerms.Add(new WorkingMemoryTerm(31, false, 2, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(32, true, 2, 3, 7));
        threeBackTerms.Add(new WorkingMemoryTerm(33, false, 2, 3, 9));
        threeBackTerms.Add(new WorkingMemoryTerm(34, false, 2, 3, 13));
        threeBackTerms.Add(new WorkingMemoryTerm(35, false, 2, 3, 9));
        threeBackTerms.Add(new WorkingMemoryTerm(36, true, 2, 3, 9));
        threeBackTerms.Add(new WorkingMemoryTerm(37, false, 2, 3, 12));
        threeBackTerms.Add(new WorkingMemoryTerm(38, false, 2, 3, 1));
        threeBackTerms.Add(new WorkingMemoryTerm(39, false, 2, 3, 4));
        threeBackTerms.Add(new WorkingMemoryTerm(40, true, 2, 3, 12));
        #endregion

        return threeBackTerms;
    }

}
