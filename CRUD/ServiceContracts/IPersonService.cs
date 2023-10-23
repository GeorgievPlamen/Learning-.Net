using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

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
        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all existing persons from the data storage
        /// </summary>
        /// <returns></returns>
        Task<List<PersonResponse>> GetAllPersons();

        /// <summary>
        /// Returns the person object based on the given Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        Task<PersonResponse?> GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// Returns all person objects that match the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">String to search with</param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">List to be sorted</param>
        /// <param name="SortBy">Name of the property the list should be sorted</param>
        /// <param name="sortOrder">Ascending or descending</param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string SortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Updates the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest"></param>
        /// <returns></returns>
        Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        /// <summary>
        /// Delets a person based on the given person id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        Task<bool> DeletePerson(Guid? personId);
        /// <summary>
        /// Returns persons as CSV
        /// </summary>
        /// <returns>Returns the memory stream with CSV data</returns>
        Task<MemoryStream> GetPersonsCSV();

        /// <summary>
        /// Returns Persons as Excel
        /// </summary>
        /// <returns>Memory stream with Excel data</returns>
        Task<MemoryStream> GetPersonsExcel();
    }
}
