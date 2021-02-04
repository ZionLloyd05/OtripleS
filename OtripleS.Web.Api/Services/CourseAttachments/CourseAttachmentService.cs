﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.CourseAttachments;

namespace OtripleS.Web.Api.Services.CourseAttachments
{
    public class CourseAttachmentService : ICourseAttachmentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public CourseAttachmentService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<CourseAttachment> RemoveCourseAttachmentByIdAsync(Guid courseId, Guid attachmentId)
        {
            CourseAttachment maybeCourseAttachment =
                await this.storageBroker.SelectCourseAttachmentByIdAsync(courseId, attachmentId);

            return await this.storageBroker.DeleteCourseAttachmentAsync(maybeCourseAttachment);
        }
    }
}