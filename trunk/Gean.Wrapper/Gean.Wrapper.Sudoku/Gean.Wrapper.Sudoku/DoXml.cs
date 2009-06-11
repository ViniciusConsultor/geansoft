using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Xml;

namespace Gean.Wrapper.Sudoku
{
    public class DoXml : AbstractXmlDocument
    {
        internal DoXml(string filepath)
            : base(filepath)
        {
        }

        public void CreatDoExerciseElement(Difficulty difficulty, Exercise exercise)
        {
            XmlDocument rootDoc = (XmlDocument)(this.BaseXmlNode);
            XmlElement dataElement = rootDoc.CreateElement("data");

            XmlElement dataChildElement;

            dataChildElement = rootDoc.CreateElement("ID");
            dataChildElement.InnerText = exercise.ID;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("Exercise");
            dataChildElement.InnerText = exercise.SingleExercise;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("Solution");
            dataChildElement.InnerText = exercise.Solution;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("CreaterEmail");
            dataChildElement.InnerText = exercise.CreaterEmail;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("PlayerEmail");
            dataChildElement.InnerText = exercise.PlayerEmail;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("SolveDuration");
            dataChildElement.InnerText = exercise.SolveDuration.ToShortTimeString();
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("SolveTime");
            dataChildElement.InnerText = exercise.SolveTime.ToString();
            dataElement.AppendChild(dataChildElement);

            this.GetDifficultyElement(difficulty).AppendChild(dataElement);
        }

        public Exercise[] SelectExercises(Difficulty difficulty)
        {
            List<Exercise> exerciseList = new List<Exercise>();
            XmlElement dataelement = this.GetDifficultyElement(difficulty);
            foreach (XmlNode node in dataelement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    break;
                }
                XmlElement ele = (XmlElement)node;
                exerciseList.Add(Exercise.Parse(ele));
            }
            return exerciseList.ToArray();
        }

        private XmlElement GetDifficultyElement(Difficulty difficulty)
        {
            return XmlHelper.GetElementByName(this.DocumentElement.SelectSingleNode("Do"), difficulty.ToString());
        }

    }

}
