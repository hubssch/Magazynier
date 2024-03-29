﻿using System;

namespace Magazynier.DatabaseAccess
{
    public enum DocumentType
    {
        WZ,
        PZ
    }

    public class ItemDocument
    {
        public int DocID { get; set; }
        public string Signature { get; set; }
        public string DocType { get; set; }
        public string ContractData { get; set; }
        public DateTime Date { get; set; }

        public ItemDocument(int docID, string signature, string docType, string contractData, DateTime date)
        {
            DocID = docID;
            Signature = signature;
            DocType = docType;
            ContractData = contractData;
            Date = date;
        }

        public ItemDocument() : this(0, "", "", "", DateTime.Now) { }
    }
}
