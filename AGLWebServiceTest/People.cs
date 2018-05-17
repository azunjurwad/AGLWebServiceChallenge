using System.Collections.Generic;
using AGLWebServiceTest.Contracts;

namespace AGLWebServiceTest
{
    public class People
    {
        /// <summary>
        /// Gets root object set creating http client instance
        /// </summary>
        /// <param name="ApiUrl">Api Url to access AGL web service</param>
        /// <returns></returns>
        public PeopleRootObject[] GetPeopleRootObject(string ApiUrl)
        {
            var localHttpClientInstance = Environment.HttpClient.Instance;
            var peopleObjects = localHttpClientInstance.LastResponseAs<PeopleRootObject[]>(ApiUrl);
            return peopleObjects;
        }

        /// <summary>
        /// Get female owner list from the web service response
        /// </summary>
        /// <param name="peopleObjects">Root object set - desearlized json</param>
        /// <returns>List of feamale owners</returns>
        public List<string> GetFemaleOwnerList(PeopleRootObject[] peopleObjects)
        {
            var petsList = GetSortedCatList(peopleObjects);
            var ownerList = GetSortedOwnerList(peopleObjects, petsList);
            return GetOwnerListByGender("female", peopleObjects, ownerList);
        }

        /// <summary>
        /// Get male owner list from the web service response
        /// </summary>
        /// <param name="peopleObjects">Root object set - desearlized json</param>
        /// <returns>List of amale owners</returns>
        public List<string> GetMaleOwnerList(PeopleRootObject[] peopleObjects)
        {
            var petsList = GetSortedCatList(peopleObjects);
            var ownerList = GetSortedOwnerList(peopleObjects, petsList);
            return GetOwnerListByGender("male", peopleObjects, ownerList);
        }

        /// <summary>
        /// Sort the pets list list as per cats names
        /// </summary>
        /// <param name="peopleObjects">Root object set - desearlized json</param>
        /// <returns>Sorted cats list</returns>
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

        /// <summary>
        /// Sort owners as per the sorting of cats list
        /// </summary>
        /// <param name="peopleObjects">Root object set - desearlized json</param>
        /// <param name="petsList">Sorted cats list</param>
        /// <returns>Sorted owners list</returns>
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

        /// <summary>
        /// Get list of owners as per their gender
        /// </summary>
        /// <param name="gender">Gender</param>
        /// <param name="peopleObjects">Root object set - desearlized json</param>
        /// <param name="owenersList">List of all owners</param>
        /// <returns>list of owners as per gender</returns>
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
