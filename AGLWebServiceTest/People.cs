using AGLWebServiceTest.Environment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AGLWebServiceTest
{
    public class People
    {
        public PeopleRootObject[] GetPeopleRootObject(string ApiUrl)
        {
            var localHttpClientInstance = Environment.HttpClient.Instance;
            var peopleObjects = localHttpClientInstance.LastResponseAs<PeopleRootObject[]>(ApiUrl);
            return peopleObjects;
        }

        public List<string> GetFemaleOwnerList(PeopleRootObject[] peopleObjects)
        {
            var petsList = GetSortedCatList(peopleObjects);
            var ownerList = GetSortedOwnerList(peopleObjects, petsList);
            return GetOwnerListByGender("female", peopleObjects, ownerList);
        }

        public List<string> GetMaleOwnerList(PeopleRootObject[] peopleObjects)
        {
            var petsList = GetSortedCatList(peopleObjects);
            var ownerList = GetSortedOwnerList(peopleObjects, petsList);
            return GetOwnerListByGender("male", peopleObjects, ownerList);
        }

        public List<string> GetSortedCatList(PeopleRootObject[] peopleObjects)
        {
            List<string> petsList = new List<string>();
            foreach (var owner in peopleObjects)
            {
                if (owner.Pets != null)
                {
                    foreach (var pet in owner.Pets)
                    {
                        if (string.Equals(pet.PetType, "Cat", System.StringComparison.OrdinalIgnoreCase))
                        {
                            petsList.Add(pet.PetName);
                        }
                    }
                }
            }

            petsList.Sort();
            return petsList;
        }

        public List<string> GetSortedOwnerList(PeopleRootObject[] peopleObjects, List<string> petsList)
        {
            List<string> owenersList = new List<string>();
            foreach (var sortedPet in petsList)
            {
                foreach (var owner in peopleObjects)
                {
                    if (owner.Pets != null)
                    {
                        foreach (var pet in owner.Pets)
                        {
                            if (string.Equals(pet.PetName, sortedPet, System.StringComparison.OrdinalIgnoreCase))
                            {
                                if (!owenersList.Contains(owner.PersonName))
                                {
                                    owenersList.Add(owner.PersonName);
                                }
                            }
                        }
                    }
                }
            }

            return owenersList;
        }

        public List<string> GetOwnerListByGender(string gender, PeopleRootObject[] peopleObjects, List<string> owenersList)
        {
            List<string> MaleOwenersList = new List<string>();
            List<string> FemaleOwenersList = new List<string>();

            foreach (var sortedOwner in owenersList)
            {
                foreach (var owner in peopleObjects)
                {
                    if (string.Equals(owner.PersonName, sortedOwner, System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(owner.Gender, "male", System.StringComparison.OrdinalIgnoreCase))
                            MaleOwenersList.Add(owner.PersonName);
                        else if (string.Equals(owner.Gender, "female", System.StringComparison.OrdinalIgnoreCase))
                            FemaleOwenersList.Add(owner.PersonName);
                    }
                }
            }

            if (string.Equals(gender, "male", System.StringComparison.OrdinalIgnoreCase))
                return MaleOwenersList;
            else
                return FemaleOwenersList;
        }
    }
}
