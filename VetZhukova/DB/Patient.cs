using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Patient
    {
        public int PatientID { get; set; }
        public int Age { get; set; }
        public int OwnerID { get; set; }
        private string Name, Species, Breed, ContactInfo, ImagePath;

        public string name { get { return Name; } set { Name = value; } }
        public string species { get { return Species; } set { Species = value; } }
        public string breed { get { return Breed; } set { Breed = value; } }
        public string contactInfo { get { return ContactInfo; } set { ContactInfo = value; } }
        public string imagePath { get { return ImagePath; } set { ImagePath = value; } }

        public Patient() { }

        public Patient(int Age, int OwnerID, string Name, string Species, string Breed, string ContactInfo, string imagePath)
        {
            this.Age = Age;
            this.OwnerID = OwnerID;
            this.Name = Name;
            this.Species = Species;
            this.Breed = Breed;
            this.ContactInfo = ContactInfo;
            this.ImagePath = imagePath;
        }
    }
}
