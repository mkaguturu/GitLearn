using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using Nop.Services.Directory;
using Nop.Services.Localization;
using SDK.ImmunoTech.Models.People;
using SDK.IT.Core.Domain.Business;
using SDK.IT.Services.Business;
using SDK.IT.Services.People;

namespace SDK.ImmunoTech.Models.Business
{
    public class TypeListModelFromMaster
    {
        public int TypeId { get; set; }

        public String Name { get; set; }
    }

    public class DistributorModel
    {
        private ILanguageService _languageService;
        private ICountryService _countryservice;
        private IStateProvinceService _stateProvinceService;
        private IDistributorService _distributorService;
        private IPersonalDetailsService _personalDetailsService;

        public DistributorModel()
        {
           
        }

        //public DistributorModel() { }

        public static implicit operator Distributor(DistributorModel model)
        {
            Distributor d = null;
            if(model.IsEntrepreneur)
            {
                d = new Entrepreneur();
            }
            else
            {
                d = new ITCustomer();
            }

            d.Id = model.DistributorId;

            if(model.SponsorId > 0)
            {
                d.SponsorId = model.SponsorId;
            }

            if(model.UplineId > 0)
            {
                d.UplineId = model.UplineId;
            }

            d.DistributorCode = model.DistributorCode;
            d.IsActive = true;

            d.SourceTypeId = model.InformationSourceTypeId;
            d.BusinessTypeId = model.BusinessTypeId;
            d.PreferredLanguageTypeId = model.LanguageTypeId;

            /*
             * Implicit conversions enabled
             * 
             * makes code more readable
             * even though we have to jump to different files to debug etc.
             */
            d.PersonalDetails = model.PersonalDetails;
            d.ContactRef = model.ContactRef;

            d.D_TaxDetails = model.D_TaxDetails;
            d.WebAdministration = model.WebAdministration;
            d.ActivityLog = model.ActivityLog;
            d.BankDetails = model.BankDetails;

            return d;
        }

        public void InitAllLists(ILanguageService languageService,
                        ICountryService countryservice,
                        IStateProvinceService stateProvinceService,
                        IPersonalDetailsService personalDetailsService,
                        IDistributorService distributorService)
        {
            _languageService = languageService;
            _countryservice = countryservice;
            _stateProvinceService = stateProvinceService;
            _personalDetailsService = personalDetailsService;
            _distributorService = distributorService;
            
            LanguagesList = from lang in _languageService.GetAllLanguages()
                            select new TypeListModel() { TypeId = lang.Id, Name = lang.Name };

            CountryList = from country in _countryservice.GetAllCountries()
                          select new TypeListModel() { TypeId = country.Id, Name = country.Name};

            GenderList = from gender in _personalDetailsService.GetGenderTypeList()
                            select new TypeListModel() { TypeId = gender.Id, Name = gender.GenderTypeCode};

            InformationSourceList = from info in _distributorService.GetDistributorInformationSourceTypeList()
                                    select new TypeListModel() { TypeId = info.Id, Name = info.InformationCode };

            BusinessTypeList = from business in _distributorService.GetDistributorBusinessTypeList() 
                               select new TypeListModel() { TypeId = business.Id, Name = business.BusinessTypeCode };

            PayTypeList = from pay in _distributorService.GetDistributorPayTypeList()
                          select new TypeListModel() { TypeId = pay.Id, Name = pay.PayTypeCode};

            GeneratedDistributorCode = _distributorService.GenerateDistributorCode();
        }

        public int DistributorId { get; set; }
        
        public int SponsorId { get; set; }

        public string SponsorName { get; set; }

        public int UplineId { get; set; }

        public string UplineName { get; set; }

        public int DistributorCode { get; set; }

        public int GeneratedDistributorCode { get; set; }

        public bool IsEntrepreneur { get; set; }

        public bool IsActive { get; set; }

        public int InformationSourceTypeId { get; set; }

        public int LanguageTypeId { get; set; }

        public int BusinessTypeId { get; set; }
        
        public int PayTypeId { get; set; }

        //public int GenderTypeId { get; set; }

        public IEnumerable<TypeListModel> LanguagesList { get; private set; }
               
        public IEnumerable<TypeListModel> CountryList { get; private set; }
               
        public IEnumerable<TypeListModel> StateProvinceList { get; private set; }

        public IEnumerable<TypeListModel> GenderList { get; private set; }

        public IEnumerable<TypeListModel> InformationSourceList { get; set; }
               
        public IEnumerable<TypeListModel> BusinessTypeList { get; private set; }
                
        public IEnumerable<TypeListModel> PayTypeList { get; set; }

        public ContactModel ContactRef { get; set; }

        public PersonModel PersonalDetails { get; set; }

        public DistributorTaxDetailsModel D_TaxDetails { get; set; }
        
        public DistributorWebAdministrationModel WebAdministration { get; set; }

        public DistributorActivityLogModel ActivityLog { get; set; }

        public DistributorBankDetailsModel BankDetails { get; set; }


        //public string NoteBook { get; set; }

        //public DistributorModel Sponsor { get; set; }

        //public DistributorModel Upline { get; set; }

        //public ICollection<Distributor> DownlineList { get; set; }

        //public ICollection<Distributor> SponsoredList { get; set; }

        //public virtual ICollection<DistributorEntityLog> LogDetails { get; set; }       
    }
}