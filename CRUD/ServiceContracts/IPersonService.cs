using System;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a new preson into the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all existing persons from the data storage
        /// </summary>
        /// <returns></returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns the person object based on the given Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        PersonResponse? GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// Returns all person objects that match the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">String to search with</param>
        /// <returns></returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
    }
}
