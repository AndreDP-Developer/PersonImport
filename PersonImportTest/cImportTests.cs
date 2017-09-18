using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using PersonImport;

namespace PersonImportTest
{
    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void personFrequencyTest()
        {
            #region Arrange
            var import = new Import();

            // Input
            List<Person> personList = new List<Person>
            {
                new Person(){ FirstName = "André", LastName = "Du Preez", Address = "22 Vink Street", PhoneNumber = "09276647" },
                new Person(){ FirstName = "Elsa", LastName = "Du Preez", Address = "22 Vink Street", PhoneNumber = "09277747" },
                new Person(){ FirstName = "André", LastName = "Du Plessis", Address = "25 Acacia Street", PhoneNumber = "07276647" },
                new Person(){ FirstName = "Hanlie", LastName = "Du Plessis", Address = "25 Acacia Street", PhoneNumber = "07286647" },
                new Person(){ FirstName = "Melanie", LastName = "Beetge", Address = "27 Koljander Street", PhoneNumber = "08283347" },
                new Person(){ FirstName = "André", LastName = "De Beer", Address = "29 Mopani Street", PhoneNumber = "08287747" },
                new Person(){ FirstName = "Linda", LastName = "De Beer", Address = "29 Mopani Street", PhoneNumber = "08288847" }
            };

            List<string> expectedList = new List<string>
            {
               "De Beer,2",
               "Du Plessis,2",
               "Du Preez,2",
               "Beetge,1"
            };
            #endregion

            #region Act
            // Act
            string strTestFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\FrequencyTest.csv";
            import.createSortedFrequencyFile(personList, strTestFile);

            // open the file
            List<string> actualList = new List<string>();

            using (var streamReader = File.OpenText(strTestFile))
            {
                // read file till end
                while (!streamReader.EndOfStream)
                {
                    actualList.Add(streamReader.ReadLine());
                }
            }
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList);
            #endregion
        }

        [TestMethod]
        public void personAddressTest()
        {
            #region Arrange
            var import = new Import();

            // Input
            List<Person> personList = new List<Person>
            {
                new Person(){ FirstName = "André", LastName = "Du Preez", Address = "22 Vink Street", PhoneNumber = "09276647" },
                new Person(){ FirstName = "Elsa", LastName = "Du Preez", Address = "22 Mossie Street", PhoneNumber = "09277747" },
                new Person(){ FirstName = "André", LastName = "Du Plessis", Address = "25 Acacia Street", PhoneNumber = "07276647" },
                new Person(){ FirstName = "Hanlie", LastName = "Du Plessis", Address = "25 Diedrik Street", PhoneNumber = "07286647" },
                new Person(){ FirstName = "Melanie", LastName = "Beetge", Address = "27 Koljander Street", PhoneNumber = "08283347" },
                new Person(){ FirstName = "André", LastName = "De Beer", Address = "29 Beer Street", PhoneNumber = "08287747" },
                new Person(){ FirstName = "Linda", LastName = "De Beer", Address = "29 Dikdikkie Street", PhoneNumber = "08288847" }
            };

            List<string> expectedList = new List<string>
            {
               "25 Acacia Street",
               "29 Beer Street",
               "25 Diedrik Street",
               "29 Dikdikkie Street",
               "27 Koljander Street",
               "22 Mossie Street",
               "22 Vink Street"
            };
            #endregion

            #region Act
            // Act
            string strTestFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AddressTest.csv";
            import.createSortedAddressFile(personList, strTestFile);

            // open the file
            List<string> actualList = new List<string>();

            using (var streamReader = File.OpenText(strTestFile))
            {
                // read file till end
                while (!streamReader.EndOfStream)
                {
                    actualList.Add(streamReader.ReadLine());
                }
            }
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList);
            #endregion
        }
    }
}
