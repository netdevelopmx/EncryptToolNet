using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSA.Data.Fuciones
{
    public static class CRYPT
    {
        public static System.Boolean IsNumeric(this System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal ||
                Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch
            {
            } // just dismiss errors but return false
            return false;
        }


        public static string Decrypt(string value)
        {
            value = value.ToLower();
            var abecedario = "abcdefghijklmnñopqrstuvwxyz";
            NSAEntities contex = new NSAEntities();
            var result = "";
            //value = value.Replace(" ", "|");
            if (value.Length > 7999)
            {
                return "";
            }

            var palabras = value.Split('|');
            var lstAbecedar = "";
            foreach (var s in palabras)
            {
                var strencryp = "";

                if (s.IsNumeric())
                {
                    int idint = int.Parse(s);
                    lstAbecedar = contex.CSTRK.FirstOrDefault(n => n.ID == idint).CS;
                }
                else
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        try
                        {
                            var pos = abecedario.IndexOf(s.ToArray()[i]);
                            strencryp = strencryp + lstAbecedar.ToArray()[pos].ToString();
                        }
                        catch (Exception)
                        {
                            strencryp = strencryp + s.ToArray()[i];
                        }
                        

                    }
                }
                result = result + strencryp + " ";
            }


            return result;
        }

        public static string Encrypt(string value)
        {
            value = value.ToLower();
            var abecedario = "abcdefghijklmnñopqrstuvwxyz";
            NSAEntities contex = new NSAEntities();
            var result = "";
            value = value.Replace(" ", "|");
            if (value.Length > 7999)
            {
                return "";
            }

            var rnd = new Random();

            var palabras = value.Split('|');

            foreach (var s in palabras)
            {
                var largo = s.Length;
                var abcinicial = rnd.Next(6000);

                var str1 = contex.CSTRK.FirstOrDefault(n => n.ID == abcinicial);
                var strencryp = "";
                for (int i = 0; i < s.Length; i++)
                {
                    try
                    {
                        strencryp = strencryp + abecedario.ToArray()[str1.CS.IndexOf(s.ToArray()[i])];
                    }
                    catch (Exception)
                    {
                        strencryp = strencryp + s.ToArray()[i];
                    }

                }

                result = result + abcinicial.ToString() + "|" + strencryp + "|";
            }


            return result;

        }

        public static void LlenaData()
        {
            NSAEntities contex = new NSAEntities();

            string newap = "";
            string ap = "abcdefghijklmnñopqrstuvwxyz";
            var rnd = new Random();

            while (newap.Length < 27)
            {
                string newletter = ap.ToArray()[rnd.Next(ap.Length)].ToString();

                ap = ap.Replace(char.Parse(newletter), char.Parse("-"));
                if (newletter != "-")
                {
                    newap = newap + newletter;
                }
            }

            contex.CSTRK.Add(new CSTRK()
            {
                CS = newap
            });
            contex.SaveChanges();
        }
    }
}