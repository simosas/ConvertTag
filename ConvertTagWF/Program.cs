using System.Text;
using System.Xml;
using ClosedXML.Excel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Collections;
namespace ConvertTagWF
{
    internal static class Program
    {
        public static Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"T_BIT_X_0","Bool"},
            {"T_BOOL", "Bool" },
            {"T_DINT" , "DInt"},
            {"T_DWORD", "DWord" },
            {"T_INT", "Int" },
            {"T_REAL", "Real" },
            {"T_TIME", "Time" },
            {"T_UDINT", "UDInt"},
            {"T_WORD", "Word" }



        };
        public static Dictionary<string, string> typesHMI = new Dictionary<string, string>()
        {
            {"T_BIT_X_0","Boolean"},
            {"T_BOOL", "Boolean" },
            {"T_DINT" , "Double"},
            {"T_DWORD", "Int16" },
            {"T_INT", "Int32" },
            {"T_REAL", "Float" },
            {"T_TIME", "DateTime" },
            {"T_UDINT", "UInt32"},
            {"T_WORD", "UInt16" }



        };


        public static Dictionary<string, string> ExcelSetup = new Dictionary<string, string>()
        {
            {"A1", "Name" },
            {"B1", "Path" },
            {"C1", "Connection" },
            {"D1", "PLC tag" },
            {"E1", "DataType" },
            {"F1", "Length" },
            {"G1", "Coding" },
            {"H1", "Access Method" },
            {"I1", "Address" },
            {"J1", "Indirect addressing" },
            {"K1", "Index tag" },
            {"L1", "Start value" },
            {"M1", "ID tag" },
            {"N1", "Display name [en-US]" },
            {"O1",  "Comment [en-US]"},
            {"P1",  "Acquisition mode"},
            { "Q1", "Acquisition cycle"},
            {"R1", "Limit Upper 2 Type" },
            {"S1",  "Limit Upper 2"},
            {"T1", "Limit Upper 1 Type" },
            {"U1",  "Limit Upper 1"},
            {"V1", "Limit Lower 1 Type" },
            {"W1",  "Limit Lower 1"},
            {"X1", "Limit Lower 2 Type" },
            {"Y1",  "Limit Lower 2"},
            {"Z1", "Linear scaling" },
            {"AA1", "End value PLC" },
            {"AB1", "Start value PLC" },
            {"AC1", "End value HMI" },
            {"AD1", "Start value HMI" },
            {"AE1", "Gmp relevant" },
            {"AF1",  "Confirmation Type"},
            {"AG1", "Mandatory Commenting"}
        };


        public static List<string> raides = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG"
        };

        public static List<string> raides_textblock = new List<string>()
        {
            "A", "B", "C", "D", "E"
        };

        public static List<string> raides_textlist = new List<string>()
        {
            "B", "C", "D", "E", "F", "G", "H"
        };


        public static Dictionary<string, int> TypeLength = new Dictionary<string, int>()
        {
            {"UInt",2 }, {"Int",2 }, {"USInt",1 }, {"String", 254}, {"DateTime", 8}, {"DInt", 4}, {"LReal", 8}, {"Real", 4}, {"Bool", 1}, {"Boolean", 1}, {"UInt16", 2}, {"Bit", 1}
        };

        static void DeltaSiemensHmiOPCUA(string file)
        {

            // delta .csv -> siemens .xlsx hmi tags
            StreamReader sr = new StreamReader(file);

            Console.Write("connection:");
            string connection = Console.ReadLine();
            Console.Write("address prefix:"); //ns=urn:Schneider:M262:customprovider;s=Application.GVL.
            string addr_prefix = Console.ReadLine();
            if (addr_prefix == string.Empty)
            {
                addr_prefix = "ns=urn:Schneider:M262:customprovider;s=Application.";// galas (Application.) gali keistis?
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hmi Tags");
                //
                var unused = workbook.Worksheets.Add("Multiplexing");
                unused.Cell("A1").Value = "HMI Tag name";
                unused.Cell("B1").Value = "Multiplex Tag";
                unused.Cell("C1").Value = "Index";
                //
                foreach (KeyValuePair<string, string> pair in ExcelSetup)
                {
                    worksheet.Cell(pair.Key).Value = pair.Value;
                }
                // name, connection, datatype, length, coding (binary/IEEE754 real lreal)
                int count = 2;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!line.Contains("Define") && line != "")
                    {
                        string[] attribs = line.Split(','); //name, type, address, comment
                        string type = string.Empty;
                        if (attribs[1] == "BIT")
                            type = "Boolean";
                        if (attribs[1] == "WORD")
                            type = "UInt16";
                        //

                        //
                        List<string> results = [attribs[0], "Default tag table", connection, "<No Value>", type, TypeLength[type].ToString(), "Binary", "Absolute access", addr_prefix + attribs[0], "False", "<No Value>", "<No Value>", "0", "<No Value>", attribs[3], "Cyclic in operation", "1 s", "None", "<No Value>", "None", "<No Value>", "None", "<No Value>", "False", "10", "0", "100", "0", "False", "None", "False"];
                        for (int i = 0; i < results.Count; i++)
                        {
                            worksheet.Cell(raides[i] + count.ToString()).Value = results[i];
                        }


                    }
                    count++;
                }
                workbook.SaveAs(file + ".xlsx");
            }
            sr.Close();



        }
        static void DeltaSiemensHmiModbus(string file)
        {

            // delta .csv -> siemens .xlsx hmi tags
            StreamReader sr = new StreamReader(file);

            Console.Write("connection:");
            string connection = Console.ReadLine();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hmi Tags");
                //
                var unused = workbook.Worksheets.Add("Multiplexing");
                unused.Cell("A1").Value = "HMI Tag name";
                unused.Cell("B1").Value = "Multiplex Tag";
                unused.Cell("C1").Value = "Index";
                //
                foreach (KeyValuePair<string, string> pair in ExcelSetup)
                {
                    worksheet.Cell(pair.Key).Value = pair.Value;
                }
                // name, connection, datatype, length, coding (binary/IEEE754 real lreal)
                int count = 2;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!line.Contains("Define") && line != "")
                    {
                        string[] attribs = line.Split(','); //name, type, address, comment
                        string type = string.Empty;
                        if (attribs[1] == "BIT")
                            type = "Bool";
                        if (attribs[1] == "WORD")
                            type = "UInt";
                        //
                        string siemens_modbus_addr = "4x4"; // 4x400001.1 - 4x465535.16
                        if (attribs[2].Contains("EtherLink"))
                        {
                            string modbus_addr = attribs[2].Remove(0, attribs[2].IndexOf('-') +1); // 1.00 || 1
                            int modbus_bit;
                            // jei randa taska, isimt taskas +1, pridet +1 prie likusio skaiciaus po tasko kad gaut bito nr
                            // skaiciu pries taska naudot kaip word adresa 
                            if (modbus_addr.IndexOf('.') != -1)
                            {
                                char temp = modbus_addr[modbus_addr.IndexOf('.') + 2];
                                modbus_bit = (temp - '0') + 1; // char tiesiogiai nesikonvertuoja i int, todel reikia atimt is '0' kad gaut skaiciu
                                modbus_addr = modbus_addr.Remove(modbus_addr.IndexOf('.'), 3);
                            }
                            else
                            {
                                modbus_bit = -1;
                            };
                            
                            for (int i = 0; i < (5 - modbus_addr.Length); i++)
                            {
                                siemens_modbus_addr += "0";
                            }
                            siemens_modbus_addr += modbus_addr;
                            if (modbus_bit != -1)
                            {
                                siemens_modbus_addr += "." + modbus_bit;
                            }
                        }

                        List<string> results;
                        if (siemens_modbus_addr != "4x4")
                        {
                            if (type == "UInt")
                                type = "Int";
                            else if (type == "Bool")
                                type = "Bit";
                            results = [attribs[0], "Default tag table", connection, "<No Value>", type, TypeLength[type].ToString(), "Binary", "Absolute access", siemens_modbus_addr, "False", "<No Value>", "<No Value>", "0", "<No Value>", attribs[3], "Cyclic in operation", "1 s", "None", "<No Value>", "None", "<No Value>", "None", "<No Value>", "False", "10", "0", "100", "0", "False", "None", "False"];
                        }
                        else
                        {

                            results = [attribs[0], "Default tag table", "<No Value>", "<No Value>", type, TypeLength[type].ToString(), "Binary", "<No Value>", "<No Value>", "False", "<No Value>", "<No Value>", "0", "<No Value>", attribs[3], "Cyclic in operation", "1 s", "None", "<No Value>", "None", "<No Value>", "None", "<No Value>", "False", "10", "0", "100", "0", "False", "None", "False"];
                        }

                        //
                       
                        for (int i = 0; i < results.Count; i++)
                        {
                            worksheet.Cell(raides[i] + count.ToString()).Value = results[i];
                        }


                    }
                    count++;
                }
                workbook.SaveAs(file + ".xlsx");
            }
            sr.Close();



        }


        static void SchneiderSiemensHmi(string file)
        {
            Console.Write("connection:");
            string connection = Console.ReadLine();
            Console.Write("address prefix:"); //ns=urn:Schneider:M262:customprovider;s=Application.GVL.
            string addr_prefix = Console.ReadLine();
            if (addr_prefix == string.Empty)
            {
                addr_prefix = "ns=urn:Schneider:M262:customprovider;s=Application.GVL.";
            }
            using (var workbook = new XLWorkbook())
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                var worksheet = workbook.Worksheets.Add("Hmi Tags");
                //
                var unused = workbook.Worksheets.Add("Multiplexing");
                unused.Cell("A1").Value = "HMI Tag name";
                unused.Cell("B1").Value = "Multiplex Tag";
                unused.Cell("C1").Value = "Index";
                //
                foreach (KeyValuePair<string, string> pair in ExcelSetup)
                {
                    worksheet.Cell(pair.Key).Value = pair.Value;
                }
                // name, connection, datatype, length, coding (binary/IEEE754 real lreal)
                int count = 2;
                foreach (XmlNode node in xmlDocument.ChildNodes[1].ChildNodes[2].ChildNodes[0].ChildNodes[0]) //tags
                {

                    string name = node.Attributes[0].Value;
                    string type = node.Attributes[1].Value;
                    if (types.ContainsKey(type))
                    {
                        type = typesHMI[type];
                    }


                    else if (type.Contains("ARRAY"))
                    {
                        string tt = node.Attributes[1].Value.Replace("__", ",");
                        tt = tt.Replace("_", ",");
                        string[] tts = tt.Split(',');
                        //2,3 - range, 5 - type
                        type = "Array [" + tts[2] + ".." + tts[3] + "] of " + typesHMI["T_" + tts[5]];
                        //sw.WriteLine("      " + name + " : " + "Array[" + tts[2] + ".." + tts[3] + "] of " + tts[5] + ";");
                    }
                    string coding = "Binary";
                    if (type == "Real" || type == "LReal")
                        coding = "IEEE754";
                    string typelength = string.Empty;

                    try
                    {
                        typelength = TypeLength[type].ToString();
                    }
                    catch
                    {
                        typelength = "2";
                    }

                    List<string> results = [name, "Default tag table", connection, "<No Value>", type, typelength, coding, "<No Value>", addr_prefix + name, "False", "<No Value>", "<No Value>", "0", "<No Value>", "<No Value>", "Cyclic in operation", "1 s", "None", "<No Value>", "None", "<No Value>", "None", "<No Value>", "False", "10", "0", "100", "0", "False", "None", "False"];

                    for (int i = 0; i < results.Count; i++)
                    {
                        worksheet.Cell(raides[i] + count.ToString()).Value = results[i];
                    }





                    count++;
                }


                workbook.SaveAs(file + ".xlsx");
            }
        }

        static void DeltaSiemensPlcDB(string file)
        {


            //
            StreamReader sr = new StreamReader(file);
            StreamWriter sw = new StreamWriter(Path.GetFileName(file) + ".db");
            sw.WriteLine("DATA_BLOCK \"" + Path.GetFileName(file) + "\"");
            sw.WriteLine("{ S7_Optimized_Access := 'TRUE' }");
            sw.WriteLine("VERSION : 0.1");
            sw.WriteLine("NON_RETAIN");
            sw.WriteLine("   VAR ");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (!line.Contains("Define") && line != "")
                {
                    string[] attribs = line.Split(','); //name, type, address, comment
                    string type = string.Empty;
                    if (attribs[1] == "BIT")
                        type = "Bool";
                    if (attribs[1] == "WORD")
                        type = "Word";
                    //
                    sw.WriteLine("      " + attribs[0] + " : " + type + ";   " + "// " + attribs[3]);
                    //



                }

            }
            sw.WriteLine("   END_VAR");

            sw.WriteLine(string.Empty);
            sw.WriteLine(string.Empty);
            sw.WriteLine("BEGIN");
            sw.WriteLine(string.Empty);
            sw.WriteLine("END_DATA_BLOCK");

            sr.Close();
            sw.Close();

        } //delta - siemens db tags


        static void SchneiderSiemensPlcDB(string file)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(file);
            StreamWriter sw = new StreamWriter(Path.GetFileName(file) + ".db");
            sw.WriteLine("DATA_BLOCK \"" + Path.GetFileName(file) + "\"");
            sw.WriteLine("{ S7_Optimized_Access := 'TRUE' }");
            sw.WriteLine("VERSION : 0.1");
            sw.WriteLine("NON_RETAIN");
            sw.WriteLine("   VAR ");
            foreach (XmlNode node in xmlDocument.ChildNodes[1].ChildNodes[1]) //array, struct
            {
                if (node.Name == "TypeArray")
                {
                    //attr 4 = type
                    sw.WriteLine("      " + node.Attributes[0].Value.Replace("_", string.Empty) + " : " + node.Attributes[4].Value + ";");
                }
                if (node.Name == "TypeUserDef") //struct
                {
                    sw.WriteLine(node.Attributes[0].Value + " : " + "Struct");
                    Console.WriteLine("UDT: " + node.Attributes[0].Value);
                    StreamWriter udt = new StreamWriter(Path.GetDirectoryName(file) + "\\" + node.Attributes[0].Value + ".udt"); // user defined type
                    udt.WriteLine("TYPE \"" + node.Attributes[0].Value + "\"");
                    udt.WriteLine("VERSION : 0.1");
                    udt.WriteLine("   STRUCT");

                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (types.ContainsKey(cnode.Attributes[1].Value))
                        {
                            sw.WriteLine("         " + cnode.Attributes[0].Value + " : " + types[cnode.Attributes[1].Value] + ";");
                            udt.WriteLine("         " + cnode.Attributes[0].Value + " : " + types[cnode.Attributes[1].Value] + ";");
                        }
                        else
                        {
                            string t = cnode.Attributes[1].Value.Replace("__", ",");
                            t = t.Replace("_", ",");
                            string[] ts = t.Split(',');
                            // 2,3 ,5 
                            sw.WriteLine("         " + cnode.Attributes[0].Value + " : " + "Array[" + ts[2] + ".." + ts[3] + "] of " + ts[5] + ";");
                            udt.WriteLine("         " + cnode.Attributes[0].Value + " : " + "Array[" + ts[2] + ".." + ts[3] + "] of " + ts[5] + ";");

                        }
                    }
                    sw.WriteLine("      END_STRUCT;");
                    udt.WriteLine("      END_STRUCT;");
                    udt.WriteLine("      END_TYPE");
                    udt.Close();
                }
            }
            foreach (XmlNode node in xmlDocument.ChildNodes[1].ChildNodes[2].ChildNodes[0].ChildNodes[0]) //tags
            {
                string name = node.Attributes[0].Value;
                string type = node.Attributes[1].Value;
                if (!type.Contains("ARRAY"))
                {
                    try
                    {
                        sw.WriteLine("      " + name + " : " + types[type] + ";");
                    }

                    catch
                    {
                        sw.WriteLine("      " + name + " : " + type + ";"); // custom struct type 
                    }

                }
                else
                {
                    string tt = node.Attributes[1].Value.Replace("__", ",");
                    tt = tt.Replace("_", ",");
                    string[] tts = tt.Split(',');
                    sw.WriteLine("      " + name + " : " + "Array[" + tts[2] + ".." + tts[3] + "] of " + tts[5] + ";");
                }

            }
            sw.WriteLine("   END_VAR");

            sw.WriteLine(string.Empty);
            sw.WriteLine(string.Empty);
            sw.WriteLine("BEGIN");
            sw.WriteLine(string.Empty);
            sw.WriteLine("END_DATA_BLOCK");
            sw.Close();
            //Console.ReadLine();

        }


        static void SchneiderSiemensProgramBlocks(string file)
        {
         
            // siemens xml export - siemens import 
            string documentation = "";
            FileInfo fi = new FileInfo(file);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(file);
            
            foreach (XmlNode node in xmlDocument.ChildNodes[1].ChildNodes[2].ChildNodes[1]) // node = program blokas
            {
                //
                string blockname = node.Attributes[0].Value;
                string returntype = "n";
                Dictionary<string,string> specialus = new Dictionary<string,string>(); // timeriai, counteriai

                    if (node.ChildNodes[0].ChildNodes.Count > 0)
                    {
                        if (node.ChildNodes[0].ChildNodes[0].Name == "returnType")
                        {
                            returntype = node.ChildNodes[0].ChildNodes[0].ChildNodes[0].Name;
                        }
                    }
                    

                 // codesys leidzia funkcijos vardui priskirt kintamaji ir taip returnint rezultata, siemens negali, reikia rast return tipa ir sukurt papildoma kintamaji kuriuo pakeiciami tie returnai
                bool turioutput = false;
                StreamWriter sw = new StreamWriter(fi.Directory + "\\"+ blockname + ".scl");
                sw.WriteLine("FUNCTION_BLOCK " + blockname);
                sw.WriteLine("{ S7_Optimized_Access := 'TRUE' }");
                sw.WriteLine("VERSION : 0.1");
                //
                // in,out, static vars: interface node'as
                foreach (XmlNode varnode in node.ChildNodes[0].ChildNodes)
                {
                    if (varnode.Name == "documentation")
                    {
                        documentation = varnode.InnerText;
                    }
                    if (varnode.Name == "inputVars")
                    {
                        // childnodes = vars
                        // childnode.attribute[0] = name
                        // childnode.childnode[0].childnode[0].Name = data type 
                        sw.WriteLine("   VAR_INPUT");
                        foreach (XmlNode variable in varnode.ChildNodes)
                        {
                            if (variable.ChildNodes[0].ChildNodes[0].Name != "derived")
                            {
                                sw.WriteLine(variable.Attributes[0].InnerText.Replace("\"", "") + " : " + variable.ChildNodes[0].ChildNodes[0].Name + ";");
                            }
                                
                            else
                            {
                                specialus.Add(variable.Attributes[0].InnerText.Replace("\"", ""), variable.ChildNodes[0].ChildNodes[0].Attributes[0].ChildNodes[0].InnerText);
                            }
                        }
                        sw.WriteLine("   END_VAR");
                    }
                    if (varnode.Name == "outputVars")
                    {
                        turioutput = true;
                        sw.WriteLine("   VAR_OUTPUT");
                        foreach (XmlNode variable in varnode.ChildNodes)
                        {
                            if (variable.ChildNodes[0].ChildNodes[0].Name != "derived")
                            {
                                sw.WriteLine(variable.Attributes[0].InnerText.Replace("\"", "") + " : " + variable.ChildNodes[0].ChildNodes[0].Name + ";");
                            }

                            else
                            {
                                specialus.Add(variable.Attributes[0].InnerText.Replace("\"", ""), variable.ChildNodes[0].ChildNodes[0].Attributes[0].ChildNodes[0].InnerText);
                            }
                        }
                        if (returntype != "n")
                            sw.WriteLine(blockname + "_out_var" + " : " + returntype + ";");
                        sw.WriteLine("   END_VAR");
                    }
                     if (varnode.Name == "localVars")
                    {
                        sw.WriteLine("   VAR");
                        foreach (XmlNode variable in varnode.ChildNodes)
                        {
                            if (variable.ChildNodes[0].ChildNodes[0].Name != "derived")
                            {
                                sw.WriteLine(variable.Attributes[0].InnerText.Replace("\"", "") + " : " + variable.ChildNodes[0].ChildNodes[0].Name + ";");
                            }
                                
                            else
                            {
                                specialus.Add(variable.Attributes[0].InnerText.Replace("\"", ""), variable.ChildNodes[0].ChildNodes[0].Attributes[0].ChildNodes[0].InnerText);
                            }
                               
                        }
                        sw.WriteLine("   END_VAR");

                    }
                }
                //
                if (!turioutput && returntype != "n")
                {
                    sw.WriteLine("   VAR_OUTPUT");
                    sw.WriteLine(blockname + "_out_var" + " : " + returntype +";");
                    sw.WriteLine("   END_VAR");
                }
                sw.WriteLine("BEGIN\n");

                sw.WriteLine("//" + documentation); // idet virsaus komentara i programos pradzia


                string programa = node.ChildNodes[1].ChildNodes[0].InnerText;

                programa = programa.Replace("END_IF", "END_IF;");
                programa = programa.Replace("END_FOR", "END_FOR;");
                programa = programa.Replace("END_WHILE", "END_WHILE;");
                programa = programa.Replace(blockname, blockname + "_out_var");
                foreach (KeyValuePair<string, string> pair in specialus)
                {
                    programa = programa.Replace(pair.Key, pair.Key + "." + pair.Value);
                }

                sw.Write(programa);

                sw.WriteLine("\nEND_FUNCTION_BLOCK");


                sw.Close(); 
                
                /*
                 * 
                 * pou attribute 0.value - pavadinimas
                 * 
                 * ST innertext - programa     
                 * node childnode 0 - interface; tikrint visus childnodes kad rast inputVars, outputVars, localVars
                 * documentation innertext - komentaras virsuj, prie interface childnodes
                 * ignoruot nezinomus tipus
                 * 
                 * node childnode 1- body; InnerText laiko programos koda
                 */
            }
        }
        static void DeltaBankSiemensList(string file)
        {
            XLWorkbook delta_wb = XLWorkbook.OpenFromTemplate(file);
            XLWorkbook siemens_wb = new XLWorkbook();
            List<string[]> BankStrings = new List<string[]>();
            List<string> BankNames = new List<string>();
            var sheets = delta_wb.Worksheets;
            foreach (var v in sheets)
            {
                int count = 3;
                while (true)
                {
                    if (v.Cell("A" + count).Value.ToString() == "")
                        break;
                    string[] temp = new string[5]; //name, ID, LT, PL, EN
                    for (int i = 0; i < 5; i++)
                    {
                        temp[i] = v.Cell(raides_textblock[i] + count).Value.ToString();
                    }
                    if (!BankNames.Contains(temp[0]))
                        BankNames.Add(temp[0]);
                    BankStrings.Add(temp);
                    count++;
                }

            }
            //

            var siemens_textlist = siemens_wb.Worksheets.Add("TextList");
            var siemens_textlistentry = siemens_wb.Worksheets.Add("TextListEntry");
            //
            siemens_textlist.Cell("A1").Value = "Name";
            siemens_textlist.Cell("B1").Value = "ListRange";
            siemens_textlist.Cell("C1").Value = "Comment [en-US]";
            siemens_textlist.Cell("D1").Value = "Comment [lt-LT]";
            siemens_textlist.Cell("E1").Value = "Comment [pl-PL]";
            //
            List<string> filler = ["Decimal", "<No value>", "<No value>", "<No value>"];
            for (int i = 0; i < BankNames.Count; i++)
            {
                siemens_textlist.Cell("A" + (i + 2)).Value = BankNames[i];
                for (int j = 0; j < 4; j++)
                {
                    siemens_textlist.Cell(raides_textlist[j] + (i + 2)).Value = filler[j];
                }
            }
            //
            siemens_textlistentry.Cell("A1").Value = "Name";
            siemens_textlistentry.Cell("B1").Value = "Parent";
            siemens_textlistentry.Cell("C1").Value = "DefaultEntry";
            siemens_textlistentry.Cell("D1").Value = "Value";
            siemens_textlistentry.Cell("E1").Value = "Text [en-US]";
            siemens_textlistentry.Cell("F1").Value = "Text [lt-LT]";
            siemens_textlistentry.Cell("G1").Value = "Text [pl-PL]";
            siemens_textlistentry.Cell("H1").Value = "FieldInfos";
            string firstbank = BankStrings[0][0];
            int bstring_count = 1;
            int kitas_count = 2;
            string is_default = "";
            foreach (string[] arr in BankStrings)
            {//name, ID, LT, PL, EN
                if (arr[0] != firstbank)
                {
                    bstring_count = 1;
                    firstbank = arr[0];
                }
                if (arr[1] == "0")
                {
                    is_default = "TRUE";
                }
                else is_default = "";
                List<string> results = ["Text_list_entry_" + bstring_count, firstbank, is_default, arr[1], arr[4], arr[2], arr[3], ""];

                for (int i = 0; i < results.Count; i++)
                {
                    siemens_textlistentry.Cell(raides[i] + kitas_count.ToString()).Value = results[i];
                }
                kitas_count++;
                bstring_count++;
            }
            siemens_wb.SaveAs("Converted_" + file + "to siemens.xlsx");


        }


        static void DeltaIspSiemensDB(string file)
        {
            // .csv failas panasus i delta dopsoft tagus 
        }

        static void DeltaIspSiemensProg(string file)
        {
            /*unzippint .mpu failus (ten .rar), isimt Unzipped.src, perskaityt kaip tekstini faila
             * skiriasi FB ir PRG tipai (?)
             * input/output vars(?)
             * atskirt LAD ir ST blokus
            */
        }

 

        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            

            /*
             * prie program blocku konvertavimu tag'u pridet DB pavadinima?
             * delta isp siemens db funkcija (nebutina)
             * delta isp siemens programa (nebutina)
             * atkartot memory mapper: paimt nukopijuota schneider var sarasa, pridet AT atmintis kur truksta, esancias AT atmintis palikt vietoj (padaryt pasirinkima?)
             * atkartot eksporta i delta, kazkaip nustatyt koks etherlink prefixas
             * padaryt winforms programa 
             * automatinis failu atpazinimas konvertavimui?
             * konvertavimas is siemens i delta/schneider(tikriausiai useless)
             
             
             */
            
            string file = string.Empty;
            try
            {
                file = args[0];
            }
            catch
            {
                Console.WriteLine("no file");
                Console.ReadLine();
                Environment.Exit(0);
            }

            // arg 0 = filename
            // detect which kinda file is dragged on
            Console.WriteLine("1. Delta DOPSoft - Siemens PLC DB");
            Console.WriteLine("2. Schneider - Siemens PLC DB");
            Console.WriteLine("3. Schneider - Siemens HMI tags");
            Console.WriteLine("4. Delta DOPSoft - Siemens HMI tags (OPC UA)");
            Console.WriteLine("5. Delta DOPSoft - Siemens HMI tags (Modbus)");
            Console.WriteLine("6. Delta DOPSoft TextBank - Siemens TextList");
            Console.WriteLine("7. Schneider - Siemens program block");
           // Console.WriteLine("7. Delta ISPSoft - Siemens PLC DB");
            //Console.WriteLine("8. Delta ISPSoft - Siemens program block");

            // delta ispsoft - siemens plc tags
            // delta ispsoft - siemens plc program (ladder - scl, ST - scl)

            ConsoleKeyInfo a = Console.ReadKey();
            Console.WriteLine();


            //file = "delta.xlsx";

            switch (a.KeyChar)
            {
                case '1':
                    DeltaSiemensPlcDB(file);
                    break;

                case '2':
                    SchneiderSiemensPlcDB(file);
                    break;

                case '3':
                    SchneiderSiemensHmi(file);
                    break;

                case '4':
                    DeltaSiemensHmiOPCUA(file);
                    break;
                case '5':
                    DeltaSiemensHmiModbus(file);
                    break;
                case '6':
                    DeltaBankSiemensList(file);
                    break;
                case '7':
                    SchneiderSiemensProgramBlocks(file);
                    break;


                default: Environment.Exit(0); break;
            }




            Console.WriteLine("end");
            Console.ReadLine();


        }
    }
}