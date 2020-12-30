using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace rasp
{
    public class TOOL
    {
        public List<List<string>> DATA = new List<List<string>>();
        const string leadsFile = "data.txt";
        public int index = 0;

        public List<List<string>> readFile()
        {
            int I = 0;
            DATA.Clear();
            StreamReader streamReader = new StreamReader(leadsFile);
            string str = "";

            while (!streamReader.EndOfStream)
            {
                str = streamReader.ReadLine();
                char[] S = new Char[] { ' ' };

                List<string> mas = new List<string>(str.Split(new Char[] { ' ' }));
                mas.Add(I.ToString());
                DATA.Add(mas);
                I += 1;
            }
            streamReader.Close();
            return DATA;
        }

        public List<List<string>> findLeads(string name, string lastname, string phone)
        {
            List<List<string>> Data = new List<List<string>>();
            foreach (List<string> i in DATA)
            {
                string patternName = @"" + name + "";
                string patternLastName = @"" + lastname + "";
                string patternPhone = @"" + phone + "";

                Regex regex1 = new Regex(patternName);
                Match match1 = regex1.Match(i[0]);

                Regex regex2 = new Regex(patternLastName);
                Match match2 = regex2.Match(i[1]);

                Regex regex3 = new Regex(patternPhone);
                Match match3 = regex3.Match(i[2]);

                if (match1.Success)
                {
                    if (match2.Success)
                    {
                        if (match3.Success)
                        {
                            List<string> tmp = new List<string>();
                            tmp.Add(i[0]);
                            tmp.Add(i[1]);
                            tmp.Add(i[2]);
                            tmp.Add(i[3]);
                            tmp.Add(i[4]);
                            tmp.Add(i[5]);
                            tmp.Add(i[6]);

                            Data.Add(tmp);
                        }
                    }
                }

            }
            return Data;
        }

        public string appendLead(string name, string lastname, string phone, string email, string adress, string birthdate)
        {
            Regex regexPhone = new Regex(@"^\d*$");
            bool resPhone = regexPhone.IsMatch(phone);

            Regex regexDate = new Regex(@"^([0-9]{4})[\-]([0]?[1-9]|[1][0-2])[\-](0?[1-9]|[12][0-9]|3[01])$");
            bool resDate = regexDate.IsMatch(birthdate);

            string error = "";
            if (resPhone && resDate)
            {

                if (name != "" && lastname != "" && email != "" && adress != "")
                {
                    bool flag = true;
                    foreach (List<string> i in DATA)
                    {
                        if (i[0] == name && i[1] == lastname && i[2] == phone && i[3] == email && i[4] == adress && i[5] == birthdate)
                        {
                            flag = false;
                            error = "Извините, но контакт с такими данными уже существует!";
                            break;
                        }
                    }

                    if (flag)
                    {
                        StreamWriter sw = new StreamWriter(leadsFile, true);
                        sw.WriteLine(name + " " + lastname + " " + phone + " " + email + " " + adress + " " + birthdate);
                        sw.Close();
                        return "OK";
                    }
                    else
                    {
                        return error;
                    }
                }
                else
                {
                    return "Введите все поля контакта!";
                }
            }
            else if (!resPhone)
            {
                return "В поле телефон должны быть только цифры!";
            }
            else
            {
                return "Неправильный формат ввода дня рождения";
            }

        }
        public void rewriteLeads()
        {
            StreamWriter sr = new StreamWriter(leadsFile, false);
            foreach (List<string> i in DATA)
            {
                sr.WriteLine(i[0] + " " + i[1] + " " + i[2] + " " + i[3] + " " + i[4] + " " + i[5]);
            }
            sr.Close();
        }

        public string editLead(string name, string lastname, string phone, string email, string adress, string birthdate)
        {
            Regex regexPhone = new Regex(@"^\d*$");
            bool resPhone = regexPhone.IsMatch(phone);

            Regex regexDate = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[\-]([0]?[1-9]|[1][0-2])[\-]([0-9]{4})$");
            bool resDate = regexDate.IsMatch(birthdate);

            if (resPhone && resDate)
            {

                if (name != "" && lastname != "" && email != "" && adress != "" && birthdate != "")
                {
                    DATA[index][0] = name;
                    DATA[index][1] = lastname;
                    DATA[index][2] = phone;
                    DATA[index][3] = email;
                    DATA[index][4] = adress;
                    DATA[index][5] = birthdate;

                    return "OK";
                }
                else
                {
                    return "Введите все поля контакта!";
                }
            }
            else if (!resPhone)
            {
                return "В поле телефон должны быть только цифры!";
            }
            else
            {
                return "Неправильный формат ввода дня рождения!";
            }
        }
    }
}
