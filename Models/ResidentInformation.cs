using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;

namespace AutoMapperDemo
{
    //Add destination call from the Mappings Class

    //var destination = Mapping.Mapper.Map<Destination>();
//Translating data from one object type to another.
    //We use DTOs, or ViewModels, or Requests/Response object from Service or Web API Call

    //Given a class Instance, but you want the values of that property to Share with another class Instance.
    //Watch:

    public class Resident
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class ResidentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        //Check AutoMapperConfig to Map ResidentDTO=>Address
    }

    public class Address
    {
        public int StreetNumberLocation { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }

    public class Phone
    {
        public int PhoneNumber { get; set; }
    }

    public class Mobile
    {
        public bool HasMobile { get; set; }
    }

    //Example:
    public class Author
    {
        public string Name { get; set; }
        //            ^^^^
        //What if later I wanted to split Name into FirstName and LastName?
        //What happens to BookViewModel's Author.Name? It BEAKS

    }

    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }

    //Normal Mapping with example above:
//    BookViewModel model = new BookViewModel {
//        Title = book.Title,
//        Author = book.Author.Name
//    }
    
    


    //When Mapping Collections!!!!!!!

    //As long as you have defined a mapping for your DesinationCalss
    //AutoMapper will be able to automatically map a collection of that type
    //  Steps:
    //  1:Specify that appropriate collection type for the Map call:
    //  "var destinationList = AutoMapper.Mapper.Map<List<DestinationClass>>(sourceList);"
    //This Works for ALL Collection types and interfaces: List<T>, ICollection<T>, IEnumerable<T> ...etc..
    

    //          CAREFUL!! WARNING!
    //      DO NOT EMPLOY the Existing instance Mapping, if so you will destroy what you  made:
    //      Example: AutoMapper.Mapper.Map(sourceList, destinationList);
    //          Kills destination list. Automapper is Mapping the Collection not the Objects within that collection, idividually

    public class Pet
    {
        public string Name { get; set; }
        public string Breed { get; set; }
    }

    public class Person
    {
        //This is also A Hint
        public List<Pet> Pets { get; set; }
    }

    public class PetDTO
    {
        public string Name { get; set; }
        public string Breed { get; set; }
    }

    public class PersonDTO
    {
        //This is Also a Hint
        public List<PetDTO> Pets { get; set; }
    }


    //We have a Person class with a collection of pets, and a DTO for EachType
    //If we created a update for just Name, What happens to Breed?
    //Does it affect both instances of Breed?
    //If So How?

    //The collection will update for Name just fine, but When The
    //Collection was updated, Breed was left in the dust, Thus Breed is NULL!
    //
    //  {Pets:
    //      {Name : "Habeh", breed: null},
    //      {Name : "Scott", breed: null},
    //      {Name : "Luke", breed: null},
    //  }
    //
    //   Probably gunna ask well how do we stop the mapping of the collection from fucking up shit
    //      use .Ignore()
    //      Ex:     AutoMapper.Mapper.CreateMap<PersonDTO, Person>()
    //              .ForMember(dest => dest.Pets,
    //                          opts => opts.Ignore());
    //          This tells AutoMapper to not mapp this PetsCollection - Handle it Manually(creating a loop for that one Case)
    //
    //      IDENTIFYING The PRoperty:
    //      var pet = person.Pets[i];
    //      var updatedPet = personDTO.Pets.Single(m => m.Id == pet.Id);
    //      AutoMapper.Mapper.Map(pet, updatedPet);

    /*
        var updatedPets = new List<Pet>();
            foreach (var pet in personDTO.Pets)
        {
            var existingPet = person.Pets.SingleOrDefault(m => m.Id == pet.Id);
            // No existing pet with this id, so add a new one
            if (existingPet == null)
            {
                updatedPets.Add(AutoMapper.Mapper.Map<Pet>(pet));
            }
            // Existing pet found, so map to existing instance
            else
            {
                AutoMapper.Mapper.Map(existingPet, pet);
                updatedPets.Add(existingPet);
            }
        }
            // Set pets to updated list (any removed items drop out naturally)
        person.Pets = updatedPets;
    */
}
