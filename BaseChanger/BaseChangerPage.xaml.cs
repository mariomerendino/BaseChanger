using Xamarin.Forms;
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
        public void Run(object Sender, EventArgs e){
            //checks signed toggle. 
            if (signed.IsToggled){
				if (dec.Text != null)
				{
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
                
            }
            else
            {

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
            }
        }
        public void Clear(object Sender, EventArgs e){
            binary.Text = null;
            octal.Text = null;
            hex.Text = null;
            dec.Text = null;
            binary.Placeholder = "Binary";
            octal.Placeholder = "Octal";
            hex.Placeholder = "Hexidecimal";
            dec.Placeholder = "Decimal";
            signed.IsToggled = false;
            
        }
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
        public String BinaryToOctal()
        {
            String b = binary.Text;
			long value = Convert.ToInt64(b, 2);
            return Convert.ToString(value, 8);
		}
		public String BinaryToHex()
		{
			String b = binary.Text;
			long value = Convert.ToInt64(b, 2);
			return Convert.ToString(value, 16);
		}
        public String DecimalToBinary(){
            String d = dec.Text;
			long value = Convert.ToInt64(d, 10);
			return Convert.ToString(value, 2);
        }
		public String HexToBinary()
		{
			String h = hex.Text;
			long value = Convert.ToInt64(h, 16);
			return Convert.ToString(value, 2);
		}
		public String OctalToBinary()
		{
			String o = octal.Text;
			long value = Convert.ToInt64(o, 8);
			return Convert.ToString(value, 2);
		}
        public String TwosCompliment()
        {
            if (int.Parse(numBits.Text) < binary.Text.Length)
            {
                DisplayAlert("Error", "Num of bits too small", "Okay");
                return " ";
            }
            else
            {
                String bin = binary.Text;
                while (int.Parse(numBits.Text) != bin.Length)
                {
                    bin = "0" + bin;

                }
                StringBuilder flip = new StringBuilder();
                char[] b = bin.ToCharArray();
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
        }
    }
}
