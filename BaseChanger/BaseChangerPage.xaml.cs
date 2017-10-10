﻿using Xamarin.Forms;
using System;
using System.Text;
namespace BaseChanger
{
    public partial class BaseChangerPage : ContentPage
    {
        public BaseChangerPage()
        {
            InitializeComponent();
        }
        //If caluclate button is clicked. 
        public void Run(object Sender, EventArgs e)
        {
            //Checks if atleast one field is enterd. 
            if ((dec.Text == "" || dec.Text == null) && (hex.Text == "" || hex.Text == null) && (octal.Text == "" || octal.Text == null) && (binary.Text == "" || binary.Text == null))
            {
                DisplayAlert("Error", "One field must be filled", "Okay");
                Clear(null, null);

            }
            //checks format 
            else if(!checkOctal() || !checkDecimal() || !checkBinary() || !checkHex()){
                
            }
            //checks signed toggle.
            else
            {
                if (signed.IsToggled)
                {
                    isSigned();
                }
                //if unsigned
                else
                {
                    notSigned();
                }
            }
            
        }
        //Function is run if the bottom toggle is on. 
        public void isSigned(){
            if (dec.Text != null){
                try
                {
                    int num;
                    if ((num = int.Parse(dec.Text)) < 0)
                    {
                        dec.Text = Math.Abs(num).ToString();
                        binary.Text = DecimalToBinary();
                        binary.Text = TwosCompliment();
                        dec.Text = "-" + dec.Text;
                        octal.Text = BinaryToOctal();
                        hex.Text = BinaryToHex();

                    }
                    else
                    {
                        binary.Text = DecimalToBinary();
                        octal.Text = BinaryToOctal();
                        hex.Text = BinaryToHex();
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", "Invalid input", "Okay");

                }

            }
            else if (binary.Text != null)
            {
                binary.Text = addZeros();
                octal.Text = BinaryToOctal();
                hex.Text = BinaryToHex();
                if(binary.Text.ToCharArray()[0]=='1'){
                    String temp = binary.Text;
                    binary.Text = TwosCompliment();
                    dec.Text = "-" + BinaryToDecimal();
                    binary.Text = temp;

                }
                else{
                    dec.Text = BinaryToDecimal();
                }
            }
            else if (octal.Text != null)
            {
                binary.Text = addZeros();
                binary.Text = OctalToBinary();
                hex.Text = BinaryToHex();

                if (binary.Text.ToCharArray()[0] == '1')
                {
                    String temp = binary.Text;
                    binary.Text = TwosCompliment();
                    dec.Text = "-" + BinaryToDecimal();
                    binary.Text = temp;

                }
                else
                {
                    dec.Text = BinaryToDecimal();
                }
            }
            else if (hex.Text != null)
            {
                binary.Text = addZeros();
                binary.Text = HexToBinary();
                octal.Text = BinaryToOctal();
                if (binary.Text.ToCharArray()[0] == '1')
                {
                    String temp = binary.Text;
                    binary.Text = TwosCompliment();
                    dec.Text = "-" + BinaryToDecimal();
                    binary.Text = temp;

                }
                else
                {
                    dec.Text = BinaryToDecimal();
                }
            }
            else{
                DisplayAlert("Error", "You must clear feilds", "Okay");
            }
        }
        //function is run if the toggle is off
        public void notSigned(){
            if (dec.Text != null)
            {
                try
                {
                    if (int.Parse(dec.Text) < 0)
                    {
                        DisplayAlert("Error", "You cannot have a negative number", "Okay");
                        Clear(null, null);
                    }
                    else
                    {
                        binary.Text = DecimalToBinary();
                        octal.Text = BinaryToOctal();
                        hex.Text = BinaryToHex();
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", "Invalid input", "Okay");

                }

            }
            else if (binary.Text != null)
            {
                dec.Text = BinaryToDecimal();
                octal.Text = BinaryToOctal();
                hex.Text = BinaryToHex();
            }
            else if (octal.Text != null)
            {
                binary.Text = OctalToBinary();
                dec.Text = BinaryToDecimal();
                hex.Text = BinaryToHex();
            }
            else if (hex.Text != null)
            {
                binary.Text = HexToBinary();
                dec.Text = BinaryToDecimal();
                octal.Text = BinaryToOctal();
            }
            else
            {
                DisplayAlert("Error", "You must clear feilds", "Okay");
            }

        }
        //Clears Datafields. 
        public void Clear(object Sender, EventArgs e){
            binary.Text = null;
            octal.Text = null;
            hex.Text = null;
            dec.Text = null;
            numBits.Text = null;
            binary.Placeholder = "Binary";
            octal.Placeholder = "Octal";
            hex.Placeholder = "Hexidecimal";
            dec.Placeholder = "Decimal";
            numBits.Placeholder = "16";
            signed.IsToggled = false;

            
        }
        //Takes binary field, and converts it to an integer. 
        public String BinaryToDecimal(){
            double ans = 0;
            int count = 0;
            char[] b = binary.Text.ToCharArray();
            for (int i = b.Length-1; i >= 0 ; i--)
            {
                if (b[i] == '1')
                {
                    ans = ans + (Math.Pow(2, count));
                }
                count++;
            }
            return ans.ToString();
        }
        //Takes the binary value and returns an octal value
        public String BinaryToOctal()
        {
            String b = binary.Text;
			long value = Convert.ToInt64(b, 2);
            return Convert.ToString(value, 8);
		}
        //Takes the binary value and returns a Hex value
		public String BinaryToHex()
		{
			String b = binary.Text;
			long value = Convert.ToInt64(b, 2);
			return Convert.ToString(value, 16);
		}
        //Takes the decimal value and returns a binary value
        public String DecimalToBinary(){
            String d = dec.Text;
			long value = Convert.ToInt64(d, 10);
			return Convert.ToString(value, 2);
        }
        //Takes the Hex value and returns a binary value
		public String HexToBinary()
		{
			String h = hex.Text;
			long value = Convert.ToInt64(h, 16);
			return Convert.ToString(value, 2);
		}
        //Takes the Octal value and returns a binary value
		public String OctalToBinary()
		{
			String o = octal.Text;
			long value = Convert.ToInt64(o, 8);
			return Convert.ToString(value, 2);
		}
        //Returns the twos compliment of the binary value
        public String TwosCompliment()
        {

                StringBuilder flip = new StringBuilder();
                char[] b = addZeros().ToCharArray();
                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] == '0')
                        flip.Append("1");
                    else
                        flip.Append("0");
                }
                int flipped = Convert.ToInt32(flip.ToString(), 2);
                int one = Convert.ToInt32("1", 2);
                return Convert.ToString(flipped + one, 2);
        }
        //Returns a binary value with the correct amount of bits, according to how many signed bits. 
        public String addZeros()
        {
            if (int.Parse(numBits.Text) < binary.Text.Length ){
                    DisplayAlert("Error", "Num of bits too small", "Okay");
                    Clear(null,null);
                    return " ";
            }
            else{
                String bin = binary.Text;
                while (int.Parse(numBits.Text) != bin.Length)
                {
                    bin = "0" + bin;

                }
                return bin;
            }
        }
        //Checks Binary Format 
        public bool checkBinary(){
            bool ans = true;
            if(binary.Text == null|| binary.Text == "")
                return ans;
            else{
                
                char[] b = binary.Text.ToCharArray();
                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] != '0' && b[i] != '1'){
                        DisplayAlert("Error", "B[i]: " + b[i].ToString() , "Okay");
                        DisplayAlert("Error", "Incorrect format", "Okay");
                        return false;
                    } 
                }
            }
            return ans;
        }
        //Checks decimal format
        public bool checkDecimal()
        {
            bool ans = true;
            if (dec.Text == null|| dec.Text == "")
                return ans;
            else
            {
                try{
                    int d = int.Parse(dec.Text);
                }
                catch(Exception e){
                    DisplayAlert("Error", "Incorrect format", "Okay");
                    return false;
                }

            }
            return ans;

        }
        //Checks hex format
        public bool checkHex()
        {
            bool ans = true;
            if (hex.Text == null|| hex.Text == "")
                return ans;
            else
            {
                char[] h = hex.Text.ToCharArray();
                for (int i = 0; i < h.Length; i++)
                {
                    ans = ((h[i] >= '0' && h[i] <= '9') ||
                           (h[i] >= 'a' && h[i] <= 'f') ||
                           (h[i]>= 'A' && h[i] <= 'F'));

                    if (!ans)
                    {
                        DisplayAlert("Error", "Incorrect format", "Okay");
                        return false;
                    }
                }
            }
            return ans;
        }
        //checks octal format
        public bool checkOctal()
        {
            bool ans = true;
            if (octal.Text == null || octal.Text =="")
                return ans;
            else
            {
                char[] o = octal.Text.ToCharArray();
                for (int i = 0; i < o.Length; i++)
                {
                    ans = (o[i] >= '0' && o[i] <= '7');

                    if (!ans)
                    {
                        DisplayAlert("Error", "Incorrect format", "Okay");
                        return false;
                    }
                }
            }
            return ans;
        }
    }
}
