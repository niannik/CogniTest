using Domain.Entities.WorkingMemoryAggregate;
using Domain.Enums;

namespace Infrastructure.Persistence;

public class WorkingMemoryTestSeedData
{
    public static List<WorkingMemoryTest> GetWorkingMemoryTests()
    {
        var workingMemoryTests = new List<WorkingMemoryTest>();

        workingMemoryTests.Add(new WorkingMemoryTest(WorkingMemoryTestType.OneBack, 1, "آزمون 1-back چیه؟\r\nتو این آزمون، تصویر حیوانات یکی‌یکی روی صفحه نمایش داده می‌شه.\r\n\r\nقانونش اینه:\r\nهر تصویر جدید رو با تصویر قبلی مقایسه کن.\r\nاگر شبیه هم بودن، دکمه مشابه رو بزن.\r\nاگر فرق داشتن، دکمه متفاوت رو بزن.\r\n\r\nفقط تصویر فعلی رو با یکی قبل از خودش مقایسه کن.\r\nنیازی نیست بیشتر از یکی عقب بری.\r\n\r\nمثلاً اگه دیدی دو بار پشت سر هم عکس فیل اومد، دکمه مشابه رو میزنی.\r\nولی اگه قبلی مثلاً سگ بود و الان گربه اومده، دکمه متفاوت رو میزنی.\r\n\r\nموفق باشی"));
        workingMemoryTests.Add(new WorkingMemoryTest(WorkingMemoryTestType.TwoBack, 2, "آزمون 2-back چیه؟\r\nدر این آزمون هم مثل 1-back، تصویر حیوانات یکی‌یکی نشون داده می‌شه.\r\nولی این بار باید هر تصویر جدید رو با دو تصویر قبل‌تر مقایسه کنی.\r\n\r\nقانونش اینه:\r\nاگر تصویر فعلی با دو تا قبلش شبیه بود، دکمه مشابه رو بزن.\r\nاگر فرق داشت، دکمه متفاوت رو بزن.\r\n\r\nمثال:\r\nتصاویر: شیر، سگ، فیل، شیر، گربه\r\nدر تصویر چهارم (شیر)، باید با تصویر دوم (سگ) مقایسه میکنی که فرق دارن و دکمه متفاوت رو میزنی.\r\nدر تصویر پنجم (گربه)، با تصویر سوم (فیل) مقایسه میکنی که فرق دارن و دکمه متفاوت رو میزنی.\r\n\r\nموفق باشی"));
        workingMemoryTests.Add(new WorkingMemoryTest(WorkingMemoryTestType.ThreeBack, 3, "آزمون 3-back چیه؟\r\nاینجا باید هر تصویر جدید رو با سه تصویر قبل‌تر مقایسه کنی.\r\n\r\nقانونش اینه:\r\nاگر تصویر فعلی با سه تا قبلش شبیه بود، دکمه مشابه رو بزن.\r\nاگر متفاوت بود، دکمه متفاوت رو بزن.\r\n\r\nمثال:\r\nتصاویر: سگ، فیل، گربه، سگ، شیر\r\nدر تصویر چهارم (سگ)، با تصویر اول (سگ) مقایسه میکنی که شبیه هستن و دکمه مشابه رو میزنی.\r\nدر تصویر پنجم (شیر)، با تصویر دوم (فیل) مقایسه میکنی که فرق دارن و دکمه متفاوت رو میزنی."));

        return workingMemoryTests;
    }
}
