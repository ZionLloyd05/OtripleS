﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtripleS.Web.Api.Models.AssignmentAttachments.Exceptions
{
    public class InvalidAssignmentAttachmentException : Exception
    {
        public InvalidAssignmentAttachmentException(string parameterName, object parameterValue)
           : base($"Invalid Assignment Attachment, " +
                 $"ParameterName: {parameterName}, " +
                 $"ParameterValue: {parameterValue}.")
        { }
    }
}