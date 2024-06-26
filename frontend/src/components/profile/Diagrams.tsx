import { Flex, Text, VStack } from "@chakra-ui/react";
import { Chart } from "primereact/chart";
import { useContext, useEffect, useState } from "react";
import {
  AuthServise,
  GradeService,
  StudentService,
  SubjectService,
} from "../../services";
import { Select, colors } from "../ui-kit";
import { LanguageContext, Translator } from "../../store";
import { Grade, Subject } from "../../types";
import { useRoles } from "../../hooks";

const gradeService = new GradeService();
const studentService = new StudentService();
const subjectService = new SubjectService();
const authService = new AuthServise();

type DiagramsProps = {
  id: string;
};

export function Diagrams({ id }: DiagramsProps) {
  const [chartDataGrades, setChartDataGrades] = useState({});
  const [chartDataCredits, setChartDataCredits] = useState({});
  const [grades, setGrades] = useState<Grade[]>();
  const [subjects, setSubjects] = useState<Subject[]>();
  const [subject, setSubject] = useState<Subject>();
  const { red, green, darkGreen, darkRed, white, darkGrey } = colors;
  const { lang } = useContext(LanguageContext);
  const roles = useRoles();

  const updateData = async () => {
    if (roles.includes("Student")) {
      const usersSubject = await subjectService.getSubjectsBySession();
      setSubjects(usersSubject);
    } else {
      const student = await studentService.getById(id);
      const subjects = await subjectService.get();
      const studentSubjects = subjects.filter(
        (subject: Subject) => student.groupEntityId === subject.groupEntityId
      );
      setSubjects(studentSubjects);
    }
  };

  const updateGrades = async () => {
    let grades = [];
    if (subject && subject.id) {
      grades = await gradeService.getBySubjectByStudent(subject.id, id);
      setGrades(grades);
    } else {
      grades = await gradeService.getByStudent(id);
      setGrades(grades);
    }
    const data = {
      labels: [
        Translator[lang.name]["marks_lower_3"],
        Translator[lang.name]["marks_highter_3"],
      ],
      datasets: [
        {
          data: [
            grades.filter((g: any) => g.value < 4).length,
            grades.filter((g: any) => g.value >= 4).length,
          ],
          backgroundColor: [darkRed, darkGreen],
          hoverBackgroundColor: [red, green],
        },
      ],
    };

    setChartDataGrades(data);

    const creditsData = {
      labels: [
        Translator[lang.name]["pass_works"],
        Translator[lang.name]["not_pass_works"],
      ],
      datasets: [
        {
          data: [
            grades?.filter((g: any) => g.type === 1 && g.value !== -1).length,
            grades?.filter((g: any) => g.type === 1 && g.value === -1).length,
          ],
          backgroundColor: ["#2555FF", "#AC43FF"],
          hoverBackgroundColor: ["#5376f4", "#ba66f9"],
        },
      ],
    };

    setChartDataCredits(creditsData);
  };

  useEffect(() => {
    updateData();
  }, []);

  useEffect(() => {
    updateGrades();
  }, [subject]);

  return (
    <VStack p="20px" gap="20px" align="stretch" bg={white} w="100%">
      <Text fontSize="24px" fontWeight="bold">
        {Translator[lang.name]["diagrams"]}
      </Text>
      <Flex gap="20px">
        <VStack align="center" w="100%" bg={darkGrey} p="20px" gap="20px">
          <Text fontSize="24px" fontWeight="bold">
            {Translator[lang.name]["marks"]}
          </Text>
          <Chart
            type="doughnut"
            data={chartDataGrades}
            options={{ cutout: "60%", borderColor: darkGrey }}
          />
        </VStack>

        <VStack align="center" w="100%" bg={darkGrey} p="20px" gap="20px">
          <Text fontSize="24px" fontWeight="bold">
            {Translator[lang.name]["credits"]}
          </Text>
          <Chart
            type="doughnut"
            data={chartDataCredits}
            options={{ cutout: "60%", borderColor: darkGrey }}
          />
        </VStack>
      </Flex>
      <Flex justify="space-between" align="center">
        <Select
          value={subject}
          setValue={(e: any) => setSubject(e)}
          placeholder={Translator[lang.name]["select_subject"]}
          options={subjects || []}
        />
      </Flex>
    </VStack>
  );
}
