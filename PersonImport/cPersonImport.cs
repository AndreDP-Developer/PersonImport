using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PersonImport
{
    public class Import
    {
        /// <summary>
        ///  This procedure will import a specified file
        /// </summary>
        /// <param name="strfile">file to be imported </param>
        public void ImportFile(string strÌnputFile)
        {
            try
            {
                // imported list storage array
                List<Person> personList = new List<Person>();

                // open the file
                using (var streamReader = File.OpenText(strÌnputFile))
                {
                    // read file till end
                    while (!streamReader.EndOfStream)
                    {
                        var personRecord = streamReader.ReadLine();

                        // ignore header line
                        if (!personRecord.Contains("Address"))
                        {
                            var personData = personRecord.Split(new[] { ',' });

                            // ignore if amount of fields does not correspond with structure expected
                            if (personData.Length == 4)
                            {
                                personList.Add(new Person() { FirstName = personData[0].ToString(), LastName = personData[1].ToString(), Address = personData[2].ToString(), PhoneNumber = personData[3].ToString() });
                            }
                        }
                    }

                    // create frequency file
                    createSortedFrequencyFile(personList, strÌnputFile.Substring(0, strÌnputFile.Length - 4) + " Frequency.csv");


                    // create address file
                    createSortedAddressFile(personList, strÌnputFile.Substring(0, strÌnputFile.Length - 4) + " Address.csv");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} exception while importing.", e);
            }
        }

        /// <summary>
        ///  This procedure creates an array with person surnames and their frequency of occurrence
        ///  Sorted first by occurrence descending and then by surname occurrence
        /// </summary>
        /// <param name="personList">list to be sorted with frequency of occurrence </param>
        /// <param name="strOutputFile">file to be created with frequency of occurrence </param>
        public void createSortedFrequencyFile(List<Person> personList, string strOutputFile)
        {
            try
            {
                // create new file sorted by frequency of occurence and second on lastname descending
                var query = from person in personList
                            group person by person.LastName into lastName
                            orderby lastName.Count() descending, lastName.Key ascending
                            select new { Frequency = lastName.Count(), LastName = lastName.Key };

                using (StreamWriter file = new StreamWriter(File.Create(strOutputFile)))
                {
                    foreach (var row in query)
                    {
                        file.WriteLine("{0},{1}", row.LastName, row.Frequency);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} exception while sorting by frequency.", e);

            }
        }

        /// <summary>
        ///  This procedure creates a file with addresses sorted by alphabetical characters
        /// </summary>
        /// <param name="personList">list to be sorted with address </param>
        /// <param name="strOutputFile">file to be created with addresses </param>
        public void createSortedAddressFile(List<Person> personList, string strOutputFile)
        {
            try
            {
                var query = from person in personList
                            orderby Regex.Replace(person.Address, @"[0-9 \-]", string.Empty)
                            select person.Address;

                using (StreamWriter file = new StreamWriter(File.Create(strOutputFile)))
                {
                    foreach (var address in query)
                    {
                        file.WriteLine("{0}", address);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} exception while sorting by frequency.", e);
            }
        }

    }
}
