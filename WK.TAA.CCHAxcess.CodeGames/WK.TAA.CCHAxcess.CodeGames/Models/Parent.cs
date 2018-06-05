using System;
using System.Collections.Generic;
using System.Text;

namespace WK.TAA.CCHAxcess.CodeGames.Models
{
    public class Parent
    {
        public Guid BatchGuid { get; set; }
        public string BatchGuidString
        {
            get
            {
                return BatchGuid.ToString();
            }
        }
        public string BatchType { get; set; }
        public string BatchStatus { get; set; }
        public string SubmittedDtTm { get; set; }
        public string CompletedDtTm { get; set; }
        public int noOfReturns { get; set; }
        public List<Child> Items { get; set; }
        public bool IsSuccessVisible
        {
            get
            {
                if (BatchStatus == "Success")
                    return true;
                return false;
            }
        }
        public bool IsFailureVisible
        {
            get
            {
                if (BatchStatus != "Success")
                    return true;
                return false;
            }
        }

    }

    public class Child
    {
        public string returnId { get; set; }
        public string status { get; set; }
        public string Comments { get; set; }
        public bool IsSuccessVisible
        {
            get
            {
                if (status == "Success")
                    return true;
                return false;
            }
        }
        public bool IsFailureVisible
        {
            get
            {
                if (status != "Success")
                    return true;
                return false;
            }
        }
    }
}
