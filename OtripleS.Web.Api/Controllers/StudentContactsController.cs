﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using OtripleS.Web.Api.Models.StudentContacts;
using OtripleS.Web.Api.Models.StudentContacts.Exceptions;
using OtripleS.Web.Api.Services.StudentContacts;
using RESTFulSense.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OtripleS.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentContactsController : RESTFulController
    {
        private readonly IStudentContactService studentContactService;

        public StudentContactsController(IStudentContactService studentContactService) =>
            this.studentContactService = studentContactService;

        [HttpPost]
        public async ValueTask<ActionResult<StudentContact>> PostStudentContactAsync(
            StudentContact studentContact)
        {
            try
            {
                StudentContact persistedStudentContact =
                    await this.studentContactService.AddStudentContactAsync(studentContact);

                return Ok(persistedStudentContact);
            }
            catch (StudentContactValidationException studentContactValidationException)
                when (studentContactValidationException.InnerException is AlreadyExistsStudentContactException)
            {
                string innerMessage = GetInnerMessage(studentContactValidationException);

                return Conflict(innerMessage);
            }
            catch (StudentContactValidationException studentContactValidationException)
            {
                string innerMessage = GetInnerMessage(studentContactValidationException);

                return BadRequest(innerMessage);
            }
            catch (StudentContactDependencyException studentContactDependencyException)
            {
                return Problem(studentContactDependencyException.Message);
            }
            catch (StudentContactServiceException studentContactServiceException)
            {
                return Problem(studentContactServiceException.Message);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<StudentContact>> GetAllStudentContacts()
        {
            try
            {
                IQueryable storageStudentContacts =
                    this.studentContactService.RetrieveAllStudentContacts();

                return Ok(storageStudentContacts);
            }
            catch (StudentContactDependencyException studentContactDependencyException)
            {
                return Problem(studentContactDependencyException.Message);
            }
            catch (StudentContactServiceException studentContactServiceException)
            {
                return Problem(studentContactServiceException.Message);
            }
        }

        [HttpGet("student/{studentId}/contact/{contactId}")]
        public async ValueTask<ActionResult<StudentContact>> GetStudentContactAsync(Guid studentId, Guid contactId)
        {
            try
            {
                StudentContact storageStudentContact =
                    await this.studentContactService.RetrieveStudentContactByIdAsync(studentId, contactId);

                return Ok(storageStudentContact);
            }
            catch (StudentContactValidationException semesterStudentContactValidationException)
                when (semesterStudentContactValidationException.InnerException is NotFoundStudentContactException)
            {
                string innerMessage = GetInnerMessage(semesterStudentContactValidationException);

                return NotFound(innerMessage);
            }
            catch (StudentContactValidationException semesterStudentContactValidationException)
            {
                string innerMessage = GetInnerMessage(semesterStudentContactValidationException);

                return BadRequest(innerMessage);
            }
            catch (StudentContactDependencyException semesterStudentContactDependencyException)
            {
                return Problem(semesterStudentContactDependencyException.Message);
            }
            catch (StudentContactServiceException semesterStudentContactServiceException)
            {
                return Problem(semesterStudentContactServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
            exception.InnerException.Message;
    }
}