using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;
using EntidadesLayout.EntNoPersistentes;

namespace LogicLayout
{
    public class CourseController
    {
        public static List<Course> GetAllCourse()
        {
            var lstObj = new List<Course>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Course>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }


            return lstObj;
        }

        public static List<CourseSon> GetAllCourseByStandardKey(string key)
        {
            var lstObj = new List<CourseSon>();
            var query = new StringBuilder();
            query.Append(" SELECT stancourse.StandardCourseKey, course.Name");
            query.Append(" FROM dbo.StandardCourse stancourse");
            query.Append(" LEFT JOIN dbo.Standards stan ");
            query.Append(" ON stancourse.StandardFk = stan.StandardKey");
            query.Append(" LEFT JOIN dbo.course course ");
            query.Append(" ON stancourse.CourseFk = course.CourseKey");

            query.Append(String.Format(" WHERE  stancourse.StandardFk = '{0}'", key));

            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<CourseSon>(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }


            return lstObj;
        }
    }
}
