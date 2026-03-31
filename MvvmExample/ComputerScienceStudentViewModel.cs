using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// notifies when a property of this class changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// the tudent that this class wraps
        /// </summary>
        private Student _student;

        /// <summary>
        /// the studetns first name
        /// </summary>
        public string FirstName => _student.FirstName;

        /// <summary>
        /// the students last name
        /// </summary>
        public string LastName => _student.LastName;

        /// <summary>
        /// the students course history
        /// </summary>
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>
        /// the students gpa
        /// </summary>
        public double GPA => _student. GPA;


        /// <summary>The student's GPA computer science</summary>
        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS")) 
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }

        /// <summary>
        /// event handler for heandlig pass forward events from the student
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void HandleStudentPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            if (e.PropertyName == nameof(Student.GPA)) 
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="student">student that we put into the private backing field</param>
        public ComputerScienceStudentViewModel(Student student) 
        {
            _student = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }

    } //end class
}
