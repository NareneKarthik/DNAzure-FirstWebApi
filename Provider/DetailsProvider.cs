using Microsoft.Extensions.Configuration;
using Pensioner_Details.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pensioner_Details.Provider
{
    public class DetailsProvider : IDetailsProvider
    {
        


        private IPensionerdetail detail;
       
        public PensionerDetail GetDetailsByAadhar(string aadhar)
        {
            detail = new PensionerRepository();
            PensionerDetail pensioner = detail.PensionerDetailByAadhar(aadhar);
            return pensioner;
        }
        public PensionerDetail AddNewPensioner(PensionerDetail pension)
        {
            detail=new PensionerRepository();
            PensionerDetail pensioner = detail.AddNewPensioner(pension);
            return pensioner;
        }
        public PensionerDetail UpdatePensioner(PensionerDetail pension, string aadhar)
        {
            detail= new PensionerRepository();
            PensionerDetail pensioner=detail.UpdatePensioner(pension,aadhar);
            return pensioner;
        }
        public PensionerDetail DeletPensioner(string aadhar)
        {
            detail = new PensionerRepository();
            PensionerDetail pensioner = detail.DeletePensioner(aadhar);
            return pensioner;
        }



    }
}
