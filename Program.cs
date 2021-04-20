using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JSONSerializationCMAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Values from VF
            string matterTypeReference = "m-12a-cmre";
            string address = "8 Wilson Street, Somewhere";
            string postcode = "LS1 5BD";
            string operatorPartyReference = "6i4seicDL";

            string clFirstNames = "Joe|Jane|Ceaser|Bill";
            string clSurnames = "Hood|Hannan|Hannan-Hood|Bloggs";

            string[] clFirstnamesArray = clFirstNames.Split('|');
            string[] clSurnamesArray = clSurnames.Split('|');

            // Build the JSON request object 
            CreateMatter_Profile myProfile = new CreateMatter_Profile
            {
                matterTypeReference = matterTypeReference,
                matterGroups = new List<MatterGroups_Profile>
                {
                    // Create matter information group profile
                    new MatterGroups_Profile
                    {
                        groupType = "MATTER",
                        members = new List<Members_Profile>
                        {
                            new Members_Profile
                            {
                                attributes = new List<Attribute_Profile>
                                {
                                    new Attribute_Profile { attrName = "address", attrValue = address },
                                    new Attribute_Profile { attrName = "postcode", attrValue = postcode }
                                }
                            }
                        }
                    },
                    // Create client information group profile
                    new MatterGroups_Profile
                    {
                        groupType = "CLIENT_INDIVIDUAL"
                    }
                },
                operatorPartyReference = operatorPartyReference
            };

            // Create a new "members" group within client information one to hold client attributes
            myProfile.matterGroups[1].members = new List<Members_Profile>();

            // Initialise a count of clients and create a new client "attributes" entry for each one
            int cc = 0;
            while (cc < clFirstnamesArray.Length)
            {
                myProfile.matterGroups[1].members.Add(new Members_Profile());
                myProfile.matterGroups[1].members[cc].attributes = new List<Attribute_Profile>();
                myProfile.matterGroups[1].members[cc].attributes.Add(new Attribute_Profile());
                myProfile.matterGroups[1].members[cc].attributes[0].attrName = "given-name";
                myProfile.matterGroups[1].members[cc].attributes[0].attrValue = clFirstnamesArray[cc];
                myProfile.matterGroups[1].members[cc].attributes.Add(new Attribute_Profile());
                myProfile.matterGroups[1].members[cc].attributes[1].attrName = "family-name";
                myProfile.matterGroups[1].members[cc].attributes[1].attrValue = clSurnamesArray[cc];
                cc++;
            }

            // Serialize to JSON and output
            var jsonRequest = JsonConvert.SerializeObject(myProfile, Formatting.Indented);
            Console.WriteLine(jsonRequest);




            /* --- MANUAL METHOD (what the while loop is actually doing in this example) ---
            
            myProfile.matterGroups[1].members.Add(new Members_Profile());
            myProfile.matterGroups[1].members[0].attributes = new List<Attribute_Profile>();
            myProfile.matterGroups[1].members[0].attributes.Add(new Attribute_Profile());
            myProfile.matterGroups[1].members[0].attributes[0].attrName = "given-name";
            myProfile.matterGroups[1].members[0].attributes[0].attrValue = "James";
            myProfile.matterGroups[1].members[0].attributes.Add(new Attribute_Profile());
            myProfile.matterGroups[1].members[0].attributes[1].attrName = "family-name";
            myProfile.matterGroups[1].members[0].attributes[1].attrValue = "Hood";

            myProfile.matterGroups[1].members.Add(new Members_Profile());
            myProfile.matterGroups[1].members[1].attributes = new List<Attribute_Profile>();
            myProfile.matterGroups[1].members[1].attributes.Add(new Attribute_Profile());
            myProfile.matterGroups[1].members[1].attributes[0].attrName = "given-name";
            myProfile.matterGroups[1].members[1].attributes[0].attrValue = "James";
            myProfile.matterGroups[1].members[1].attributes.Add(new Attribute_Profile());
            myProfile.matterGroups[1].members[1].attributes[1].attrName = "family-name";
            myProfile.matterGroups[1].members[1].attributes[1].attrValue = "Hood";*/



        }
    }

    class CreateMatter_Profile
    {
        public string matterTypeReference { get; set; }
        public List<MatterGroups_Profile> matterGroups { get; set; }
        public string operatorPartyReference { get; set; }
    }

    class MatterGroups_Profile
    {
        public string groupType { get; set; }
        public List<Members_Profile> members { get; set; }
    }

    class Members_Profile
    {
        public List<Attribute_Profile> attributes { get; set; }
    }

    class Attribute_Profile
    {
        public string attrName { get; set; }
        public string attrValue { get; set; }
    }
}
