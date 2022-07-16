namespace Magazynier.DatabaseAccess
{
    public class ItemContract
    {
        public int ContractID { get; set; }

        public string Name { get; set; }

        public string NIP { get; set; }

        public string Street { get; set; }

        public string Post { get; set; }

        public string Code { get; set; }

        public ItemContract() : this(0, "", "", "", "", "") { }

        public ItemContract(int contractID, string name, string nip, string street, string post, string code)
        {
            ContractID = contractID;
            Name = name;
            NIP = nip;
            Street = street;
            Post = post;
            Code = code;
        }

        public ItemContract(DbContract contract)
        {
            ContractID = contract.ContractID;
            Name = contract.Name;
            NIP = contract.NIP;
            Street = contract.Street;
            Post = contract.Post;
            Code = contract.Code;
        }
    }
}
