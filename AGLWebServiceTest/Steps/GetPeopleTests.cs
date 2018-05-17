using AGLWebServiceTest.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AGLWebServiceTest.Steps
{
    [Binding]
    public class GetPeopleTests
    {
        public static PeopleRootObject[] peopleObjects;
        public static List<string> PetsList = new List<string>();
        public static List<string> OwenersList = new List<string>();

        [Given(@"I get all owners and pets from the web service")]
        public void GivenIGetAllOwnersAndPetsFromTheWebService(Table apiTable)
        {
            foreach (var row in apiTable.Rows)
            {
                var people = new People();
                peopleObjects = people.GetPeopleRootObject(row["ApiUrl"]);
            }
        }

        [When(@"I sort sort owners alphabetically by pets name")]
        public void WhenISortSortOwnersAlphabeticallyByPetsName()
        {
            var people = new People();
            PetsList = people.GetSortedCatList(peopleObjects);
            OwenersList = people.GetSortedOwnerList(peopleObjects, PetsList);
        }

        [Then(@"I should get '(.*)' male owners")]
        public void ThenIShouldGetMaleOwners(int number)
        {
            var people = new People();
            var maleOwners = people.GetOwnerListByGender("male", peopleObjects, OwenersList);
            Assert.AreEqual(number, maleOwners.Count);
        }

        [Then(@"I should get '(.*)' female owners")]
        public void ThenIShouldGetFemaleOwners(int number)
        {
            var people = new People();
            var maleOwners = people.GetOwnerListByGender("female", peopleObjects, OwenersList);
            Assert.AreEqual(number, maleOwners.Count);
        }

        [Then(@"I should get '(.*)' and '(.*)' as first and second male owners respectively")]
        public void ThenIShouldGetAndAsFirstAndSecondMaleOwnersRespectively(string first, string second)
        {
            var people = new People();
            var maleOwners = people.GetOwnerListByGender("male", peopleObjects, OwenersList);
            List<string> expectedOwners = new List<string>();
            Assert.IsTrue(string.Equals(first, maleOwners[0], System.StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(string.Equals(second, maleOwners[1], System.StringComparison.OrdinalIgnoreCase));
        }

        [Then(@"I shoudl get feamale owners in expected order")]
        public void ThenIShoudlGetFeamaleOwnersInExpectedOrder(Table ownerOrderTable)
        {
            var people = new People();
            var femaleOwners = people.GetOwnerListByGender("female", peopleObjects, OwenersList);
            foreach (var row in ownerOrderTable.Rows)
            {
                Assert.IsTrue(string.Equals(row["owner"], femaleOwners[int.Parse(row["counter"])], System.StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
