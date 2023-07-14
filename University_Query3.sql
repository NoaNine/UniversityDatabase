USE UNIVERSITY
GO

SELECT COURSES.NAME, STUDENTS.FIRST_NAME, STUDENTS.LAST_NAME
FROM (COURSES 
INNER JOIN GROUPS
ON COURSES.COURSE_ID = GROUPS.COURSE_ID)
INNER JOIN STUDENTS
ON GROUPS.GROUP_ID = STUDENTS.GROUP_ID