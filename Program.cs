using System;
using System.Collections.Generic;

namespace Homework3
{
    
    class Node 
    {
        public char Instruction;
        public char Data;
        public Node Naxt;
        public Node(char instruction, char data) 
        {
            Instruction = instruction;
            Data = data;
            Naxt = null;
        }
    }
    class Queue
    {
        private Node root;
        public void Push(Node node)
        {
            if (root == null) { root = node; }
            else
            {
                Node ptr = root;
                while (ptr.Naxt != null)
                {
                    ptr = ptr.Naxt;
                }
                ptr.Naxt = node;

            }
        }
        public Node Pop()
        {
            if (root == null) { return null; }
            Node node = root;
            root = root.Naxt;
            node.Naxt = null;
            return node;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            string input = "";
            Queue queue = new Queue();
            Node InstructionandData;
            int ProcessCount = 0;

            while (true)
            {
                input = Console.ReadLine();
                if (input[0] == '?') { break; }
                InstructionandData = new Node(input[0], input[1]);
                queue.Push(InstructionandData);
            }
            CPUprocesss( ref queue, ProcessCount);
        }

        static void CPUprocesss( ref Queue queue, int ProcessCount)
        {
            int PushTemCount = 0,CPUaddListCount=0;
            Queue Temporaryqueue = new Queue(); 
            Node firstInstructionandData = queue.Pop();
            List<Node> CPUlist = new List<Node>();
            CPUlist.Add(firstInstructionandData);
            int duplicate_Instruction = 0;


            while (true)
            {
                int duplicate_row = 1, unique_row = 1, column = 1, real_row = 1;
                Node NextInstructionandData = queue.Pop();

                if (NextInstructionandData == null) { break; }

                foreach (Node readlist in CPUlist)
                {
                    
                    if (NextInstructionandData.Instruction == readlist.Instruction )
                    {
                        if (NextInstructionandData.Data == readlist.Data)
                        {
                             column = 0; duplicate_Instruction--; break;
                        }
                        else 
                        {
                            column++;
                            if (column == 2) { duplicate_Instruction++; }
                        }
                        
                    }
                    else 
                    {
                        if (column == 1) { duplicate_row++;}
                        unique_row++;
                    }

                }
               
                if(column == 0) { real_row = 0; }
                else if (column > 1) { real_row = duplicate_row; }
                else { real_row = unique_row - duplicate_Instruction;}

                if (real_row >= 1 && real_row <= 4 && column >= 1 && column <= 3)
                {
                       Console.WriteLine("ADD");
                        CPUlist.Add(NextInstructionandData); CPUaddListCount++;
                    
                }
               else if(column>3||real_row>4)
                {
                    Console.WriteLine("P2");
                    Temporaryqueue.Push(NextInstructionandData); PushTemCount++; 
                }
            }

            if (PushTemCount > 0)
            {
                ProcessCount++; 
                CPUprocesss(ref Temporaryqueue, ProcessCount);
            }
            else 
            { 
                ProcessCount++; Console.WriteLine("CPU cycles needed: " + ProcessCount); 
            }
           
        }
    }
    
}