using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.IO;
using CompatiblTestApp.Models;

namespace CompatiblTestApp.Models
{
    public class Data
    {
        public List<List<string>> TableData = new List<List<string>>();

        public void UploadFromFile(FileStream file)
        {
            StreamReader Reader = new StreamReader(file);
            List<string> OneRowData = new List<string>();
            string DataString = "0";
            string SubString = "";
            bool InTheQuotation = false;

            DataString = Reader.ReadLine();
            for (int j = 0; DataString != null; j++)
            {
                OneRowData = new List<string>();
                SubString = "";

                for (int i = 0; i < DataString.Length; i++)
                {
                    if (DataString[i] == '\"')
                    {
                        if (InTheQuotation == true)
                            InTheQuotation = false;
                        else
                            InTheQuotation = true;
                    }

                    if (DataString[i] == ',' && !InTheQuotation)
                    {
                        OneRowData.Add(SubString);
                        SubString = "";
                    }
                    else
                        SubString = SubString + DataString[i];
                }
                OneRowData.Add(SubString);
                TableData.Add(OneRowData);

                DataString = Reader.ReadLine();
            }
        }

        public int CountColumnsInFile(FileStream file)
        {
            StreamReader Reader = new StreamReader(file);
            int NumberOfColumns = 1;
            string DataString = "";
         
            DataString = Reader.ReadLine();

            for (int i = 0; i < DataString.Length; i++)
                if (DataString[i] == ',')
                    NumberOfColumns++;
            return NumberOfColumns;
        }

        public List<List<string>> SearchInFile(FileStream file, List<string> arg)
        {
            List<List<string>> Matches = new List<List<string>>();
            Matches.Add(TableData[0]);


            bool IsEqual = true;
            bool FulfillsTheArgument = true;


            for (int i = 1; i < TableData.Count; i++)
            {
                for (int j = 0; j < arg.Count; j++)
                {
                    if (arg[j] != "")
                    {
                        int MaxEquality = System.Math.Min(arg[j].Length, TableData[i][j].Length);
                        for (int k = 0; k < MaxEquality; k++)
                        {
                            if (arg[j][k] != TableData[i][j][k])
                            {
                                IsEqual = false;
                                break;
                            }
                        }
                        if (!IsEqual)
                        {
                            FulfillsTheArgument = false;
                            IsEqual = true;
                            break;
                        }
                    }
                }
                if (FulfillsTheArgument)
                {
                    Matches.Add(TableData[i]);
                }
                FulfillsTheArgument = true;
            }
            return Matches;
        }

    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
