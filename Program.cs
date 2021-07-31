using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {

        static async Task Main(string[] args)
        {
            #region Defaul Values
            string addresspath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            addresspath = addresspath.Remove(0, 6);
            string fileNameTests = "";
            string fileNameValues = "";
            //string fileNameTests = addresspath+"\\tests.json";
            //string fileNameValues = addresspath + "\\values.json";
            string fileNameReport  = addresspath + "\\report.json";
            #endregion Defaul Values

            if (args.Length != 2)
                Console.WriteLine("Error");
            else
            {
                fileNameTests = args[0];
                fileNameValues = args[1];
            }

            //generate sample
            /*List<Tests> SerializeTestsJsonList = GenerateTests();
            SerializeTestsJsonFail(SerializeTestsJsonList, fileNameTests);
            List<Values> serializeValuesJsonList = GenerateValues();
            SerializeValuesJsonFail(serializeValuesJsonList, fileNameValues);*/

            //read Json
            var stringTestsFronFile = GetJsonfromFile(fileNameTests);
            List<Tests> listTests = DesearilizarFronJsonFail(stringTestsFronFile);            
            stringTestsFronFile = GetJsonfromFile(fileNameValues);
            List<Values> listValues = DesearilizarValuesFronJsonFail(stringTestsFronFile);

            //logic generate report
            foreach (var tests in listTests)
            {
                foreach (var test in tests.values)
                {
                    foreach (var values in listValues)
                    {
                        foreach (var value in values.values)
                        {
                            if (value.id == test.id)//Fail or passed
                            {
                                test.value = value.value;
                            }
                            if (value.id== tests.id)
                            {
                                tests.value = value.value;
                            }
                        }
                        
                    }
                }
                
            }

            SerializeTestsJsonFail(listTests, fileNameReport);
            Console.Write(GetJsonfromFile(fileNameReport));
            Console.ReadKey();



        }
        # region GetJsonfromFil
        public static string GetJsonfromFile(string adresFile)
        {
            string JsonFromFile;
            using (var reder = new StreamReader(adresFile))
            {
                JsonFromFile = reder.ReadToEnd();
            }
            return JsonFromFile;
        }
        public static List<Tests> DesearilizarFronJsonFail(string JsonFromFail)
        {
            var list = JsonConvert.DeserializeObject<List<Tests>>(JsonFromFail);
            return list;
        }

        public static List<Values> DesearilizarValuesFronJsonFail(string JsonFromFail)
        {
            var list = JsonConvert.DeserializeObject<List<Values>>(JsonFromFail);
            return list;
        }
        #endregion

        #region generate  sample
        public static List<Tests> GenerateTests()
        {
            List<Tests> tests = new List<Tests>
            {
                new Tests
                {
                    id="122",
                    title="Security test",
                    value="",
                    values = new List<Test>
                    {
                        new Test{id="5321", title="Confidentiality",value=""},
                        new Test{id="5321", title="Integrity",value=""}

                    }
                },
                new Tests
                {
                    id="155",
                    title="Security test sad",
                    value="",
                    values = new List<Test>
                    {
                        new Test{id="4321", title="Confidentiality",value=""},
                        new Test{id="4322", title="Integrity",value=""}

                    }
                }
            };
            return tests;
        }

        public static List<Values> GenerateValues()
        {
            List<Values> values = new List<Values>
            {
                new Values{ values= new List<Value> { new Value { id = "122", value = "failed" }, new Value { id = "5321", value = "failed" }, new Value { id = "5322", value = "passed" } } },
                new Values{ values= new List<Value> { new Value { id = "155", value = "passed" }, new Value { id = "4321", value = "passed" }, new Value { id = "4322", value = "passed" } } }
                

            };
            return values;

        }            
        
        public static void SerializeValuesJsonFail(List<Values> listvalues, string fileName)
        {
            string listValuesJsonString = JsonConvert.SerializeObject(listvalues.ToArray(), Formatting.Indented);
            File.WriteAllText(fileName, listValuesJsonString);
        }
        public static void SerializeTestsJsonFail(List<Tests> listTests, string fileName)
        {
            string listTestsJsonString = JsonConvert.SerializeObject(listTests.ToArray(),Formatting.Indented);
            File.WriteAllText(fileName, listTestsJsonString);
        }
        #endregion

        #region FileAdress 
        public static string GenerateFileAdress()
        {
            return "";
        }
        #endregion

    }
}
