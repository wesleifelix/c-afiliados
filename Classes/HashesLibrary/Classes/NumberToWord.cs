using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashesLibrary.Classes
{
    public class NumberToWord
    {
        public static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            word = centos(Number);
                            pos = (numDigits % 3) + 1;
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Mil ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Milhão ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Bilhão ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        public static String ConvertWholeNumberReal(String Number, int numDigits)
        {
            string word = "";
            try
            {
                  
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    int pos = 0;//store digit grouping   
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);

                            break;
                        case 2://tens' range    
                            word = tens(Number);
     
                            break;
                        case 3://hundreds' range    
                            word = centos(Number);
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            word = ones(Number);
                            pos = (numDigits % 4) + 1;
                            place = " Mil ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
 
                            place = " Milhão ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:
                            place = " Bilhão ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            break;
                    }
                    
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "Um";
                    break;
                case 2:
                    name = "Dois";
                    break;
                case 3:
                    name = "Três";
                    break;
                case 4:
                    name = "Quatro";
                    break;
                case 5:
                    name = "Cinco";
                    break;
                case 6:
                    name = "Seis";
                    break;
                case 7:
                    name = "Sete";
                    break;
                case 8:
                    name = "Oito";
                    break;
                case 9:
                    name = "Nove";
                    break;
            }
            return name;
        }

        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Dez";
                    break;
                case 11:
                    name = "Onze";
                    break;
                case 12:
                    name = "Doze";
                    break;
                case 13:
                    name = "Treze";
                    break;
                case 14:
                    name = "Quatorze";
                    break;
                case 15:
                    name = "Quinze";
                    break;
                case 16:
                    name = "Dezeseis";
                    break;
                case 17:
                    name = "Desessete";
                    break;
                case 18:
                    name = "Dezoito";
                    break;
                case 19:
                    name = "Dezenove";
                    break;
                case 20:
                    name = "Vinte";
                    break;
                case 30:
                    name = "Trinta";
                    break;
                case 40:
                    name = "Quarenta";
                    break;
                case 50:
                    name = "Cinqueta";
                    break;
                case 60:
                    name = "Sessenta";
                    break;
                case 70:
                    name = "Setenta";
                    break;
                case 80:
                    name = "Oitenta";
                    break;
                case 90:
                    name = "Noventa";
                    break;
                case 100:
                    name = "Cento";
                    break;
                case 200:
                    name = "Duzentos";
                    break;
                case 300:
                    name = "Trezentos";
                    break;
                case 400:
                    name = "Quantrocentos";
                    break;
                case 500:
                    name = "Quinhentos";
                    break;
                case 600:
                    name = "Seiscentos";
                    break;
                case 700:
                    name = "Setecentos";
                    break;
                case 800:
                    name = "Oitocentos";
                    break;
                case 900:
                    name = "Novecentos";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String centos(String Number)
        {
            int _Number = Convert.ToInt32(Number.Substring(0,1));
            _Number *= 100;
            String name = null;
            switch (_Number)
            {
                case 100:
                    name = "Cento";
                    break;
                case 200:
                    name = "Duzentos";
                    break;
                case 300:
                    name = "Trezentos";
                    break;
                case 400:
                    name = "Quantrocentos";
                    break;
                case 500:
                    name = "Quinhentos";
                    break;
                case 600:
                    name = "Seiscentos";
                    break;
                case 700:
                    name = "Setecentos";
                    break;
                case 800:
                    name = "Oitocentos";
                    break;
                case 900:
                    name = "Novecentos";
                    break;
               
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        public  String ConvertToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if(decimalPlace < 0)
                {
                    decimalPlace = numb.IndexOf(",");
                }
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "e";// just to separate whole numbers from points/cents    
                        endStr = "centavos " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1} {2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }

        public String ConvertToWordsReais(String numb)
        {
            String var = "";
            String endStr = "";
            numb = numb.Replace(".", "");
            try
            {
                int x = 0;
           
                for (int i = numb.Length; i > 0; i--)
                {
                    if( i - x > 2)
                        endStr += ConvertWholeNumberReal(numb.Substring(x, i).Trim(), i) + " e ";
                    else
                        endStr += ConvertWholeNumberReal(numb.Substring( x, numb.Length - 1).Trim(), i);

                    x++;
                }

                //val = String.Format("{0} Reais {1}  {2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return endStr;
        }



        public String ConvertToWordsCentavos(String numb)
        {
            String var = "";
            String endStr = "";
            try
            {
               

                
               endStr += ConvertWholeNumber(numb) ;
                    

                //val = String.Format("{0} Reais {1}  {2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return endStr;
        }

        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }

        public String ConvertToWordsReais2(String numb)
        {
            String var = "";
            string endStr = "";
            numb = numb.Replace(".", "");
            try
            {
                endStr = ConvertDecimals2(numb) ;
                   

                //val = String.Format("{0} Reais {1}  {2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return endStr;
        }

        private static String ConvertDecimals2(String number)
        {
           

            int _pos = number.Length;


            if(_pos == 1)
            {
                return ones(number);
            }
            if(_pos == 2)
            {
                if( int.Parse(number) <= 20)
                {
                    return tens(number);
                }
                else
                {
                    string _sub = number[0].ToString() + "0";
                    string _sub2 = number[1].ToString();
                    String _ret = String.Format("{0} e {1}", tens(_sub), ones(_sub2));
                    return _ret;
                }

            }

            if (_pos == 3)
            {
                try
                {
                    string cent = "", dez = "", unt = "";
                    string _ret = "";
                    string _sub = number[0].ToString() + "00";

                    cent = centos(_sub);
                    int _posd = int.Parse(number[1].ToString());
                    if (_posd < 2)
                    {
                        string _subten = number.Remove(0, 1);
                        dez = centos(_subten);
                    }
                    else
                    {
                        string _subten = number[1].ToString() + "0";
                        string _sub2 = number[2].ToString();

                        dez = tens(_subten);
                        unt = ones(_sub2);

                    }

                    _ret = String.Format("{0} e {1} {2}", cent, dez, unt);
                    return _ret;
                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }
            if (_pos >= 4 && _pos <=6)
            {
                try
                {
                    string milhar = "", cent = "", dez = "", unt = "";
                    string _ret = "";
                    switch (_pos)
                    {
                        case 4:
                            string mil = number[0].ToString();
                            milhar = ones(mil);
                            number = number.Remove(0, 1);
                           
                            string _sub = number[0].ToString() + "00";

                            cent = centos(_sub);
                            int _posd = int.Parse(number[1].ToString());
                            if (_posd < 2)
                            {
                                string _subten = number.Remove(0, 1);
                                dez = centos(_subten);
                            }
                            else
                            {
                                string _subten = number[1].ToString() + "0";
                                string _sub2 = number[2].ToString();

                                dez = tens(_subten);
                                unt = ones(_sub2);

                            }
                            break;
                        case 5:
                            int dezmil = int.Parse(number[0].ToString());
                            if (dezmil < 2)
                            {
                                string _subten = number.Remove(0, 1);
                                milhar = tens(_subten);
                            }
                            else
                            {
                                string _subten = number[0].ToString() + "0";
                                string _sub2 = number[1].ToString();

                                dez = tens(_subten);
                                unt = ones(_sub2);
                                milhar = dez + " e " + unt;
                            }

                            number = number.Remove(0, 2);

                            _sub = number[0].ToString() + "00";

                            cent = centos(_sub);
                            _posd = int.Parse(number[1].ToString());
                            if (_posd < 2)
                            {
                                string _subten = number.Remove(0, 1);
                                dez = centos(_subten);
                            }
                            else
                            {
                                string _subten = number[1].ToString() + "0";
                                string _sub2 = number[2].ToString();

                                dez = tens(_subten);
                                unt = ones(_sub2);

                            }


                            break;

                        case 6:

                            dezmil = int.Parse(number[0].ToString());
                            if (dezmil < 2)
                            {
                                string _subten = number.Remove(0, 1);
                                milhar = centos(_subten);
                            }
                            else
                            {
                                string _centos = centos(number[0].ToString() + "00");
                                string _subten = number[1].ToString() + "0";
                                string _sub2 = number[2].ToString();

                                dez = tens(_subten);
                                unt = ones(_sub2);
                                milhar = _centos + " e "+ dez + " e " + unt;
                            }

                            number = number.Remove(0, 3);

                            _sub = number[0].ToString() + "00";

                            cent = centos(_sub);
                            _posd = int.Parse(number[1].ToString());
                            if (_posd < 2)
                            {
                                string _subten = number.Remove(0, 1);
                                dez = centos(_subten);
                            }
                            else
                            {
                                string _subten = number[1].ToString() + "0";
                                string _sub2 = number[2].ToString();

                                dez = tens(_subten);
                                unt = ones(_sub2);

                            }

                            break;
                    }
                    
                    

                    _ret = String.Format("{0} mil {1} {2} {3}",milhar, (String.IsNullOrEmpty(cent)) ? "" :"e "+ cent, (String.IsNullOrEmpty(dez)) ? "" : "e " + dez, (String.IsNullOrEmpty(unt)) ? "" :  unt);
                    return _ret;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }





            return "zero";
        }
    }
}
