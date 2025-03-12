using System.Text;
using System.Xml;
using ClosedXML.Excel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Collections;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Vml;
using System.Text.RegularExpressions;


namespace ConvertTagWF
{
    public partial class Form1 : Form
    {
        public static bool[] mem = new bool[10485600];

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


        public static Dictionary<string, int> SchneiderDataTypes = new Dictionary<string, int>()
        {
            {"BOOL", 8 }, {"BYTE", 8}, {"WORD", 16}, {"DWORD", 32}, {"LWORD", 64}, {"SINT", 8}, {"USINT", 8},
            {"INT", 16 }, {"UINT", 16}, {"DINT", 32}, {"UDINT", 32}, {"LINT", 64}, {"ULINT", 64}, {"REAL", 32},
            {"LREAL", 64 }, {"STRING", 64}, {"WSTRING", 256}, {"TIME", 32}

        };
        public static Dictionary<string, char> SchneiderTypeMemIdentifiers = new Dictionary<string, char>()
        {
            {"BOOL", 'X' }, {"BYTE", 'B'}, {"WORD", 'W'}, {"DWORD", 'D'}, {"LWORD", 'L' }, {"SINT", 'B'}, {"USINT", 'B'},
            {"INT", 'W' }, {"UINT", 'W'}, {"DINT", 'D'}, {"UDINT", 'D'}, {"LINT", 'L'}, {"ULINT", 'L'}, {"REAL", 'D'},
            {"LREAL", 'L' }, {"STRING", 'B'}, {"WSTRING", 'W'}, {"TIME", 'D'}

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
                            string modbus_addr = attribs[2].Remove(0, attribs[2].IndexOf('-') + 1); // 1.00 || 1
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


        static void SchneiderSiemensHmiOPC(string file)
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


                    else if (type.Contains("[") && type.Contains("]") && type.Contains("]"))
                    {
                        string tt = node.Attributes[1].Value.Replace("__", ",");
                        tt = tt.Replace("_", ",");
                        string[] tts = tt.Split(',');
                        //2,3 - range, 5 - type
                        type = "Array [" + tts[2] + ".." + tts[3] + "] of " + typesHMI["T_" + tts[5]];
                        //sw.WriteLine("      " + name + ":  " + "Array[" + tts[2] + ".." + tts[3] + "] of " + tts[5] + ";");
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
            StreamWriter sw = new StreamWriter(System.IO.Path.GetFileName(file) + ".db");
            sw.WriteLine("DATA_BLOCK \"" + System.IO.Path.GetFileName(file) + "\"");
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
            StreamWriter sw = new StreamWriter(System.IO.Path.GetFileName(file) + ".db");
            sw.WriteLine("DATA_BLOCK \"" + System.IO.Path.GetFileName(file) + "\"");
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
                    StreamWriter udt = new StreamWriter(System.IO.Path.GetDirectoryName(file) + "\\" + node.Attributes[0].Value + ".udt"); // user defined type
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
                if (!type.Contains("[") && type.Contains("]"))
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
                Dictionary<string, string> specialus = new Dictionary<string, string>(); // timeriai, counteriai

                if (node.ChildNodes[0].ChildNodes.Count > 0)
                {
                    if (node.ChildNodes[0].ChildNodes[0].Name == "returnType")
                    {
                        returntype = node.ChildNodes[0].ChildNodes[0].ChildNodes[0].Name;
                    }
                }


                // codesys leidzia funkcijos vardui priskirt kintamaji ir taip returnint rezultata, siemens negali, reikia rast return tipa ir sukurt papildoma kintamaji kuriuo pakeiciami tie returnai
                bool turioutput = false;
                StreamWriter sw = new StreamWriter(fi.Directory + "\\" + blockname + ".scl");
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
                    sw.WriteLine(blockname + "_out_var" + " : " + returntype + ";");
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

 


        static (int, int, string) ParseArray(string arr)
        {
            int arr_min = 0, arr_max = 0;
            string array_tipas = "";
            Regex limitsregex = new Regex(@"\d+"); // skaiciu tikrinimo regex
            Regex datatyperegex = new Regex(@"OF\s+(\w+)"); // rast duomeni tipa po teksto "OF"

            Match match = datatyperegex.Match(arr);

            var datatype = match.Groups[1].Value.ToUpper();

            MatchCollection matches = limitsregex.Matches(arr);

            if (matches.Count >= 2) // jei yra 2 matches tai reiskias array dydis deklaruotas skaiciais, jei nera 2 tada ignore
            {
                arr_min = int.Parse(matches[0].Value);
                arr_max = int.Parse(matches[1].Value);

                array_tipas = datatype;
            }
            else
            {
                Console.WriteLine("Array su tag'u, ignore");
            }
            return (arr_min, arr_max, array_tipas);
        }

        static int CheckAndFillMem(int length, int standardsize)
        {
            //Console.WriteLine("checking mem for length: " + length);
            // fill all mem behind last tag?


            for (int i = 0; i < mem.Length; i++)
            {
                bool can_fill = true;
                if (!mem[i] && (i % standardsize) == 0)
                {
                    for (int j = i; j < i + length; j++)
                    {
                        if (mem[j] == true)
                        {
                            can_fill = false;
                            break;
                        }
                            

                    }
                    if (can_fill)
                    {
                        for (int j = i; j < i + length; j++)
                        {
                            //Console.WriteLine("Setting true:" + j);
                            if (mem[j] == true)
                                throw new Exception("uh oh duplicate");
                            mem[j] = true;
                        }
                        int ret_value = Convert.ToInt32(Math.Ceiling((i) / (standardsize * 1.0)) * standardsize);
                        //int ret_value = Convert.ToInt32(Math.Ceiling((i-length) / (standardsize * 1.0)));
                        // fill all backwards
                        for (int j = (i + length-1); j > 0; j--)
                            mem[j] = true;
                        
                        return ret_value;
                    }
                    
                    /*
                    if (startindex == -1)
                        startindex = i;
                    consecutive++;
                    if (consecutive == length)
                    {
                        //Console.WriteLine("found " + length + " spaces at " + (i));
                        for (int j = startindex; j < startindex + consecutive; j++)
                        {
                            //Console.WriteLine("Setting true:" + j);
                            mem[j] = true;
                        }


                        return (i + 1 - consecutive);
                    }*/




                }


                

            }

            throw new Exception("ner atminties?");



        }
        static string SchneiderMemMapper(string text, bool remap) // text is clipboard 
        {
           
            // string/wstring default 80, arba nurodytas dydis skliaustuose +
            // pasirinkt ar is naujo + 
            // sukurt ui 
            // array of bool neveikia, isskyrus struct'uose esantys, BET paprasti array veikia ir turetu but mappinami 
            // priskirt array prie alarm arba ignore
            // array paimt tik pirma kint bet deklaruot atminti kaip viso array (modbus leidzia max array dydi 100)
            // 
            // 

            // 1,048,560 bit (65535 word * 16)
            /* schneider atmintis skirstoma i M+ 
             * X - bit (1) (is tikruju 8 nes schneider dalykai)
             * B - byte (8)
             * W - word (16)
             * D - double word (32)
             * L - long (64)
             * long uzima 8 baitus bet gali prasitest toliau. jei uzima daugiau nei 8, rezervuoja toliau esancius 8 net jei neuzpildo ju
             * struct = long, reikia rast juos kopijuotam tekste ir priskirt kaip nauja duomenu tipa suskaiciavus vidini kintamuju dydi (long rezervacija galioja)
             * skaiciuojant struct dydi, reikia isivaizduoti kad tai vyksta vidinej atminty. pvz byte-double-byte gaunasi 8+32+8 = 48 bitai, taciau lieka laisva vieta tarp pirmo- 
             * byte ir double, kuria reikia uzpildyt. tada gaunasi 32 + 32 + 8 = 72 bitai, uzims 2 long atmintis 
             * data tipus ToUpper del visa ko
             */

            /*
             * laisvos vietos radimo ciklas
             * assume kad viskas yra is global vars, netikrint
             * string ir wstring deklaruojami su skliaustais nurodanciais dydi. string 1 char = 1 byte, wstring 1 char = 1 word
             * string = char array, t.y. string(5) = 5 baitai prasidedantys nuo MBx, wstring(5) = 5 wordai nuo MWx
             */


            var lines = text.Split(new[] { '\r', '\n' });
            

            //rast struct, sudet i duomenu tipu sarasa 
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("TYPE") && lines[i].Contains(":"))
                {
                    string struct_name = lines[i].Replace("TYPE", "").Replace(":", "").Replace(" ", "").ToUpper(); // isfiltruot TYPE, tarpus ir  : kad gaut pavadinima
                    double struct_memlength = 0; // visada bus int bet double reikalingas skaiciavimui veliau
                    if (struct_name == "STRUC_SYSTEM")
                        Console.Write("");
                    int last_tag_size = 0;
                    while (!lines[i].Contains("END_TYPE"))
                    {
                        if (lines[i].Contains(":") && lines[i].Contains(";")) // kazkur reikia tikrint ar ne komentaras
                        {
                            string varname = lines[i].Substring(0, lines[i].IndexOf(":"));

                            
                            // paimt viska tarp : ir ; , isimt tarpus, padaryt didziasias raides
                            string type = (lines[i].Substring(lines[i].IndexOf(':') + 1, lines[i].IndexOf(";") - (lines[i].IndexOf(':') + 1))).ToUpper();
                            if (type.Contains(":="))
                                type = type.Remove(type.IndexOf("=") - 1); //atsikratyt priskyrimu
                            if (!type.Contains("[") && !type.Contains("]"))
                                type = type.Replace(" ", "");
                            if (type.ToUpper().Contains("WSTRING"))
                            {
                                int string_mem_usage = 0;
                                if (type.Contains("(") && type.Contains(")"))
                                {
                                    // jei wstring dydis deklaruotas skliaustuose:
                                    string string_length = type.Substring(type.IndexOf('(') + 1, type.IndexOf(')') - type.IndexOf('(') - 1); //paimt skaiciu tarp skliausteliu
                                    string_mem_usage = Convert.ToInt32(string_length) * 16;
                                    struct_memlength += string_mem_usage; //1 word per char
                                }
                                else
                                {
                                    // default wstring dydis (80 simboliu po 1 word)
                                    string_mem_usage = 80 * 16;
                                    struct_memlength += string_mem_usage;
                                }
                                



                            }
                            else if (type.ToUpper().Contains("STRING"))
                            {
                                int string_mem_usage = 0;
                                if (type.Contains("(") && type.Contains(")"))
                                {
                                    // jei string dydis deklaruotas skliaustuose:
                                    string string_length = type.Substring(type.IndexOf('(') + 1, type.IndexOf(')') - type.IndexOf('(') - 1); //paimt skaiciu tarp skliausteliu
                                    string_mem_usage = Convert.ToInt32(string_length) * 8; // 1 byte per char
                                    struct_memlength += string_mem_usage;
                                }
                                else
                                {
                                    // default string dydis (80 simboliu po 1 byte)
                                    string_mem_usage = 80 * 8;
                                    struct_memlength += string_mem_usage;
                                }
                                
                            }
                            else
                            {
                                if (type.ToUpper().Contains("["))
                                {
                                    //Console.WriteLine(i);
                                    Regex limitsregex = new Regex(@"\d+"); // skaiciu tikrinimo regex
                                    Regex datatyperegex = new Regex(@"OF\s+(\w+)"); // rast duomeni tipa po teksto "OF"
                                    int array_mem_usage = 0;
                                    Match match = datatyperegex.Match(type);

                                    var datatype = match.Groups[1].Value.ToUpper();

                                    MatchCollection matches = limitsregex.Matches(type);

                                    if (matches.Count >= 2) // jei yra 2 matches tai reiskias array dydis deklaruotas skaiciais, jei nera 2 tada ignore
                                    {
                                        int arr_min = int.Parse(matches[0].Value);
                                        int arr_max = int.Parse(matches[1].Value);
                                        array_mem_usage = (arr_max + 1 - arr_min) * SchneiderDataTypes[datatype];
                                        struct_memlength += array_mem_usage;
                                        last_tag_size = SchneiderDataTypes[datatype];
                                        i++;

                                        
                                        
                                        continue;

                                    }
                                    else
                                    {
                                        Console.WriteLine("array su tag dydzio deklaracija"); // gali sukelt problemu?
                                        i++; continue;
                                    }
                                }
                               
                                type = type.Replace(" ", "");
                                struct_memlength += SchneiderDataTypes[type];
                                
                            }
                            // tikrinimas cia
                            if (last_tag_size == 0)
                                last_tag_size = SchneiderDataTypes[type];
                            
                            if (last_tag_size < SchneiderDataTypes[type] && struct_memlength % SchneiderDataTypes[type] != 0)
                            {
                                Console.WriteLine(last_tag_size + " yra mazesnis uz " + SchneiderDataTypes[type]);
                                struct_memlength += (SchneiderDataTypes[type] - last_tag_size);
                            }

                            last_tag_size = SchneiderDataTypes[type];
                            Console.WriteLine(struct_memlength);
                            

                        }

                        i++;
                    }
                    //var ab = Math.Ceiling(65.0 / 64.0) * 64;
                    // galbut nereikalingas?
                    struct_memlength = Math.Ceiling(struct_memlength / 64.0) * 64; // padalint is 64, suapvalint i virsu, padaugint is 64 kad gaut kiek atminties rezervuos
                    try
                    {
                        SchneiderDataTypes.Add(struct_name, Convert.ToInt32(struct_memlength - 7)); // nezinau kodel -7 bet kazkur prisideda tiek per daug ir atimant susitaiso
                        SchneiderTypeMemIdentifiers.Add(struct_name, 'L');
                    }
                    catch
                    {
                        // struct jau idetas
                    }
                    

        
                  
                   
                    Console.WriteLine("struct " + struct_name + " length " + struct_memlength);
                }
            }


            // jei remap, ismeta senas atmintis 
            bool remap_is_naujo = remap;
            if (remap_is_naujo)
            {
                //Console.WriteLine(" Mappint iš naujo");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("%M"))
                    {
                        //Console.WriteLine("Remap:" + lines[i]);
                        lines[i] = RemoveMapped(lines[i]);
                    }

                }
            }

            // rast jau sumapintus kintamuosius jei ne remap
            foreach (string line in lines)
            {
                if (line.Contains("%M") && !remap_is_naujo)
                {
                    char identifier = line[line.IndexOf('%') + 2]; // rast kokia raide eina po %M
                    int arr_min = 0, arr_max = 0;
                    string array_tipas = string.Empty;
                    string type = (line.Substring(line.IndexOf(':') + 1, line.IndexOf(";") - (line.IndexOf(':') + 1))).Replace(" ", "").ToUpper();
                    if (type.ToUpper().Contains("ARRAY"))
                    {
                        if (type.ToUpper().Contains("BOOL"))
                        {

                            continue;
                        }
                        (arr_min, arr_max, array_tipas) = ParseArray(type);


                    }
                    int mem_dydis;
                    if (array_tipas == string.Empty)
                    {
                        switch (identifier)
                        {
                            case 'X':
                                mem_dydis = 8;
                                break;
                            case 'B':
                                mem_dydis = 8;
                                break;
                            case 'W':
                                mem_dydis = 16;
                                break;
                            case 'D':
                                mem_dydis = 32;
                                break;
                            case 'L':
                                try
                                {
                                    mem_dydis = SchneiderDataTypes[type];
                                }
                                catch
                                {
                                    mem_dydis = 0;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Tipas " + type + " skippinamas");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    continue;


                                }
                                break;
                            default: throw new Exception("bruh");

                        }
                    }
                    else
                    {
                        mem_dydis = (arr_max + 1 - arr_min) * SchneiderDataTypes[array_tipas];
                    }



                    //tikrint string/wstring
                    if (type.Contains("WSTRING"))
                    {
                        string string_length = type.Substring(type.IndexOf('(') + 1, type.IndexOf(')') - type.IndexOf('(') - 1); //paimt skaiciu tarp skliausteliu
                        mem_dydis = Convert.ToInt32(string_length) * 8; //1 byte per char


                    }
                    else if (type.Contains("STRING"))
                    {
                        string string_length = type.Substring(type.IndexOf('(') + 1, type.IndexOf(')') - type.IndexOf('(') - 1); //paimt skaiciu tarp skliausteliu
                        mem_dydis = Convert.ToInt32(string_length) * 16; // 1 word per char
                    }





                    // %MX100.0 = rast %, eit 3 i prieki (skaiciaus pradzia), eiti iki : zenklo  kad gaut skaiciu
                    string s = line.Substring(line.IndexOf('%') + 3, line.IndexOf(':') - (line.IndexOf('%') + 3));
                    // gautas rezultatas gali but 101.0 tai pirma convert i double ir tik tada i int kad isvengt klaidos
                    s = s.Replace(" ", "");
                    int mem_addr = Convert.ToInt32(Convert.ToDouble(s));
                    int temp = mem_addr * mem_dydis;
                    //Console.WriteLine("mem:" + temp + " to " + (temp + mem_dydis));
                    for (int i = temp; i < (temp + mem_dydis); i++)
                    {
                        mem[i] = true;
                    }

                    string varname = RemoveMapped(line);
                    varname = varname.Substring(0, varname.IndexOf(':'));
                    


                }

            }

            // sumappint likusius
            // ieskot raktiniu zodziu kad nepradet mappint kintamuju vidury struct
            string new_lines = "";
            bool galima_mappint = true;

            foreach (string line in lines)
            {
                string newline = line;
                if (line.Contains("TYPE") && line.Contains(":"))
                {
                    galima_mappint = false;
                }
                else if (line.Contains("VAR_GLOBAL"))
                {
                    galima_mappint = true;
                }

                if (galima_mappint)
                {

                    if (line.Contains(":") && line.Contains(";") && !line.ToUpper().Contains("AT ")) //&& !line.ToUpper().Contains("[")
                    {
                        // kintamojo pav, tipo, atminties uzemimo gavimas
                        string array_tipas = string.Empty;
                        int standard_tag_size = 0; // reikalingas veliau kad paskirstyti tag'us atmintyje
                        int arr_min = 0, arr_max = 0;
                        string var_name, var_type;
                        int var_mem_length = 0;
                        var_name = line.Substring(0, line.IndexOf(':'));
                        if (!line.ToUpper().Contains("ARRAY"))
                            var_type = (line.Substring(line.IndexOf(':') + 1, line.IndexOf(";") - (line.IndexOf(':') + 1))).Replace(" ", "").ToUpper();
                        else var_type = (line.Substring(line.IndexOf(':') + 1, line.IndexOf(";") - (line.IndexOf(':') + 1))).ToUpper();

                        if (var_type.ToUpper().Contains("ARRAY"))
                        {
                            if (var_type.ToUpper().Contains("BOOL"))
                            {

                                new_lines += line + "\n";

                                continue;
                            }
                            (arr_min, arr_max, array_tipas) = ParseArray(var_type);
                            var_mem_length = (arr_max + 1 - arr_min) * SchneiderDataTypes[array_tipas];
                            standard_tag_size = SchneiderDataTypes[array_tipas];
                            //


                        }
                        else if (var_type.ToUpper().Contains("WSTRING"))
                        {

                            if (var_type.Contains("("))
                            {
                                string string_length = var_type.Substring(var_type.IndexOf('(') + 1, var_type.IndexOf(')') - var_type.IndexOf('(') - 1);
                                
                                var_mem_length = Convert.ToInt32(string_length) * 16;
                                
                            }
                            else
                            {
                                var_mem_length = 80 * 16; // default wstring length
                            }
                            var_type = "WSTRING";
                            standard_tag_size = 16;


                        }
                        else if (var_type.ToUpper().Contains("STRING"))
                        {
                            if (var_type.Contains("("))
                            {
                                string string_length = var_type.Substring(var_type.IndexOf('(') + 1, var_type.IndexOf(')') - var_type.IndexOf('(') - 1);

                                var_mem_length = Convert.ToInt32(string_length) * 8;
                            }
                            else
                            {
                                var_mem_length = 80 * 8;
                            }

                            standard_tag_size = 8;
                            var_type = "STRING";

                        }
                        else
                        {
                            try
                            {
                                var_mem_length = SchneiderDataTypes[var_type];
                                if (var_mem_length > 64)
                                    standard_tag_size = 64;
                                else standard_tag_size = SchneiderDataTypes[var_type];

                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Tipas " + var_type + " skippinamas");
                                Console.ForegroundColor = ConsoleColor.White;
                                continue;

                            }

                        }

                        //laisvos atminties vietos radimas
                        //double uzimtibitai = 0;
                        while (true)
                        {
                            //Console.WriteLine(var_name + var_type +  var_mem_length);
                            if (var_name.Contains("//"))
                            {
                                break;
                            }
                            // gaut standartini tag dydi
                            // array - array_tipas + schneiderdatatypes
                            // struct - 64
                            // string - 8, wstring - 16
                            // visi kiti is schneiderdatatypes


                            
                            double free_mem_index = CheckAndFillMem(var_mem_length, standard_tag_size);

                            // gautas bitas / var mem ilgio rounded up 

                            int l = Convert.ToInt32(Math.Ceiling((free_mem_index) / standard_tag_size));
                            //int l = Convert.ToInt32(free_mem_index);
                            //Console.WriteLine("Free index: " + free_mem_index + "  uzimami bitai:" + var_mem_length + "  atminties vieta  %M" + SchneiderTypeMemIdentifiers[var_type] + l);
                            //uzimtibitai += l;
                            //Console.WriteLine("Uzimti bitai: " +  uzimtibitai);
                            //Console.Write("");

                            if (var_type == "BOOL")
                            {

                                newline = var_name + "AT %M" + SchneiderTypeMemIdentifiers[var_type] + l + ".0" + ":  " + var_type + ";";
                                


                            }
                            else
                            {
                                if (array_tipas != string.Empty)
                                {
                                    newline = var_name + " AT %M" + SchneiderTypeMemIdentifiers[array_tipas] + l + ":  " + "ARRAY[" + arr_min + ".." + arr_max + "] OF " + array_tipas + ";";
                                    
                                }
                                else
                                {
                                    newline = var_name + "AT %M" + SchneiderTypeMemIdentifiers[var_type] + l + ":  " + var_type + ";";
                                    
                                }
                                    

                            }

                            //Console.WriteLine(var_name + ":  " + var_type + ":  " + SchneiderTypeMemIdentifiers[var_type] + ":  L=" + l + "   memlength=" + var_mem_length);
                            break;


                        }
                    }
                }
                newline = newline.Replace("\n", "");
                new_lines += newline + "\n";
            }




            return new_lines;




        }
        static string RemoveMapped(string line)
        {
            string newline = line;
            if (newline.Contains("%M"))
            {
                int endind = newline.IndexOf(':');
                int startind = 0;
                for (int i = endind; i > 0; i--)
                {
                    if (newline[i] == 'A' && newline[i+1] == 'T' && newline[i-1] == ' ') // negrazus sprendimas bet turetu veikt
                        startind = i;
                }
                newline = newline.Remove(startind, endind - startind);
            }
            return newline;
        }


        [STAThread]
        static void SendToClip(string text)
        {
            Clipboard.SetText(text);

        }

        static void SchneiderSiemensHmiModbus(string path)
        {

        }


        public Form1()
        {
            /* reset mem 
             * 
             * schneider tags struct, nes visus kitus reiks konvertuot vistiek 
             * uzpildyt schneidertags sarasa mappinimo metu?
             * tikriausiai niekas nenaudos failo exportu, bet juos vistiek reiktu supportint
             * kintamuju sarasas tuscias jei ne struct
             * atminties uzemima struct skaiciuot sudejus visus bendrai
             * skaitant schneider iskart pagriebt visus structus ir paskirt jiems atminties dydzius kad butu galima struct viduj struct apskaiciuot?
             * reikalingi formatai: siemens hmi - siemens plc (db?) - delta hmi (modbus) - schneider (tekstiniu formatu, mapped)

             * 

             * atkartot memory mapper: paimt nukopijuota schneider var sarasa, pridet AT atmintis kur truksta, esancias AT atmintis palikt vietoj (padaryt pasirinkima?)
             * atkartot eksporta i delta, kazkaip nustatyt koks etherlink prefixas
             * padaryt winforms programa 
             * automatinis failu atpazinimas konvertavimui?
             * konvertavimas is siemens i delta/schneider(tikriausiai useless)
             * prie program blocku konvertavimu tag'u pridet DB pavadinima?
             * delta isp siemens db funkcija (nebutina)
             * delta isp siemens programa (nebutina)
             * schneider - siemens modbus (realiai paimt i delta - siemens), kazkaip sukapot structus
             
             */
            InitializeComponent();

        }

        private void InputToOutputButton_Click(object sender, EventArgs e) /// >>> mygtukas
        {
            mem = new bool[10485600];
            try
            {
                string result = "";
                OutputTextBox.Text = SchneiderMemMapper(InputTextBox.Text, RemapCheckBox.Checked);
                result += "Mapped vars ;";
                if (SendToClipCheckbox.Checked)
                {
                    SendToClip(OutputTextBox.Text);
                    result += "Sent to clipboard ;";
                }
                ResultsTextBox.Text += DateTime.Now + ": " + result + "\n";
            }
            catch (Exception ex)
            {
                ResultsTextBox.Text += DateTime.Now + ":  " + ex.Message + "\n";
            }



        }

        private void ShowResultsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ResultsTextBox.Visible = ShowResultsCheckbox.Checked;
        }

        private void OutToSiemensModbus_Click(object sender, EventArgs e)
        {
            SaveFileDialog siemensmodbusfile = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };
            if (siemensmodbusfile.ShowDialog() == DialogResult.OK)
            {
                string path = siemensmodbusfile.FileName;
                SchneiderSiemensHmiModbus(path);
            }
        }
    }
}
