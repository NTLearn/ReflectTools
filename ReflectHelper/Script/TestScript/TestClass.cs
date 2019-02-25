using System.Collections.Generic;


namespace ReflectHelper
{
    /*==============================================================
    * Copyright 2018 Tencent Inc. 
    *
    *  作者：Zach (zachzhong@21kunpeng.com)
    *  时间：2019/2/1 15:36:18
    *  文件名：TestClass
    *  版本：V1.0.1  
    *  说明： 
    ========================================*/

        public enum Number
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven
    }

    public class TestClass
    {
        public string str1 { get; set; }

        public int int1 { get; set; }
        public long long1 { get; set; }
        public uint uint1 { get; set; }
        public float float1 { get; set; }
        public double double1 { get; set; }
        public List<int> Listint1 { get; set; }
        public List<string> Liststring1 { get; set; }
        public TestSecClass secondClass { get; set; }
    }

    public class TestSecClass {
        public int int2 { get; set; }
        public long long2 { get; set; }
        public string str2 { get; set; }
        public TestThrClass thirdClass { get; set; }
    }

    public class TestThrClass
    {
        public int int3 { get; set; }
        public List<TestFourthClass> ListFourth3 { get; set; }
        public string str3 { get; set; }
        public Number number3 { get; set; }
    }

    public class TestFourthClass
    {
        public int int4 { get; set; }
        public long long4 { get; set; }
        public string str4 { get; set; }
    }

}
