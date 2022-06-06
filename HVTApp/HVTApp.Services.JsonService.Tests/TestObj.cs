namespace HVTApp.Services.JsonService.Tests
{
    public class TestObj
    {
        public string S1 { get; set; }
        public int I1 { get; set; }

        public TestObj2 TestObj2 { get; set; } = new TestObj2();
    }

    public class TestObj2
    {
        public string S1 { get; set; } = "t2";
        public int I1 { get; set; } = 22;
    }

}