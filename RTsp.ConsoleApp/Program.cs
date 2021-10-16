﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Google.OrTools.ConstraintSolver;
using Google.Protobuf.WellKnownTypes;
using Solver = RTsp.Application.Solver;

namespace RTsp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var costMatrix = new int[][]
            //                 {
            //                     new[] {0, 200, 2, 2, 2, 2},
            //                     new[] {200, 0, 2, 2, 2, 2},
            //                     new[] {2, 2, 0, 2, 2, 2},
            //                     new[] {2, 2, 2, 0, 2, 2},
            //                     new[] {2, 2, 2, 2, 0, 2},
            //                     new[] {2, 2, 2, 2, 2, 0},
            //                 };

            //var costMatrix = new int[][]
            //                 {
            //                     new[] {0, 39, 22, 59, 54, 33, 57, 32, 89, 73, 29, 46, 16, 83, 120, 45, 24, 32, 36, 25, 38, 16, 43, 21, 50, 57, 46, 72, 121, 73},
            //                     new[] {39, 0, 20, 20, 81, 8, 49, 64, 63, 84, 10, 61, 25, 49, 81, 81, 58, 16, 72, 60, 78, 24, 69, 18, 75, 88, 68, 44, 83, 52},
            //                     new[] {22, 20, 0, 39, 74, 18, 60, 44, 71, 73, 11, 46, 6, 61, 99, 61, 37, 10, 51, 40, 59, 5, 62, 7, 57, 78, 51, 51, 100, 56},
            //                     new[] {59, 20, 39, 0, 93, 27, 51, 81, 48, 80, 30, 69, 45, 32, 61, 97, 75, 31, 89, 78, 97, 44, 83, 38, 84, 100, 77, 31, 63, 42},
            //                     new[] {54, 81, 74, 93, 0, 73, 43, 56, 104, 76, 76, 77, 69, 111, 72, 46, 56, 84, 49, 53, 33, 69, 12, 69, 64, 7, 69, 122, 73, 114},
            //                     new[] {33, 8, 18, 27, 73, 0, 45, 61, 71, 88, 8, 63, 22, 57, 87, 77, 54, 18, 68, 56, 71, 20, 61, 13, 75, 80, 68, 52, 90, 60},
            //                     new[] {57, 49, 60, 51, 43, 45, 0, 85, 88, 115, 52, 103, 60, 75, 64, 85, 79, 63, 83, 78, 70, 58, 38, 52, 103, 49, 102, 81, 69, 92},
            //                     new[] {32, 64, 44, 81, 56, 61, 85, 0, 74, 43, 55, 23, 40, 81, 97, 17, 8, 50, 8, 7, 23, 41, 53, 48, 19, 53, 17, 70, 92, 63},
            //                     new[] {89, 63, 71, 48, 104, 71, 88, 74, 0, 38, 69, 51, 75, 16, 35, 75, 77, 61, 77, 80, 90, 76, 116, 76, 58, 98, 57, 19, 33, 16},
            //                     new[] {73, 84, 73, 80, 76, 88, 115, 43, 38, 0, 81, 28, 72, 53, 55, 38, 49, 70, 42, 50, 53, 75, 83, 80, 24, 69, 27, 49, 51, 39},
            //                     new[] {29, 10, 11, 30, 76, 8, 52, 55, 69, 81, 0, 55, 16, 57, 91, 71, 48, 11, 62, 50, 68, 14, 64, 9, 67, 81, 61, 49, 93, 56},
            //                     new[] {46, 61, 46, 69, 77, 63, 103, 23, 51, 28, 55, 0, 44, 59, 81, 32, 26, 46, 29, 29, 45, 47, 76, 53, 15, 73, 9, 49, 77, 40},
            //                     new[] {16, 25, 6, 45, 69, 22, 60, 40, 75, 72, 16, 44, 0, 67, 105, 56, 33, 16, 46, 35, 53, 2, 57, 9, 54, 72, 48, 57, 106, 60},
            //                     new[] {83, 49, 61, 32, 111, 57, 75, 81, 16, 53, 57, 59, 67, 0, 39, 88, 82, 51, 87, 85, 103, 67, 113, 65, 70, 109, 67, 12, 39, 19},
            //                     new[] {120, 81, 99, 61, 72, 87, 64, 97, 35, 55, 91, 81, 105, 39, 0, 84, 104, 90, 93, 104, 90, 104, 82, 99, 79, 70, 82, 50, 4, 51},
            //                     new[] {45, 81, 61, 97, 46, 77, 85, 17, 75, 38, 71, 32, 56, 88, 84, 0, 23, 67, 9, 21, 15, 57, 48, 64, 19, 41, 23, 80, 81, 70},
            //                     new[] {24, 58, 37, 75, 56, 54, 79, 8, 77, 49, 48, 26, 33, 82, 104, 23, 0, 44, 14, 3, 25, 34, 51, 41, 25, 54, 23, 70, 100, 65},
            //                     new[] {32, 16, 10, 32, 84, 18, 63, 50, 61, 70, 11, 46, 16, 51, 90, 67, 44, 0, 58, 47, 67, 16, 72, 15, 59, 88, 52, 42, 90, 47},
            //                     new[] {36, 72, 51, 89, 49, 68, 83, 8, 77, 42, 62, 29, 46, 87, 93, 9, 14, 58, 0, 12, 16, 48, 48, 55, 19, 45, 21, 77, 89, 69},
            //                     new[] {25, 60, 40, 78, 53, 56, 78, 7, 80, 50, 50, 29, 35, 85, 104, 21, 3, 47, 12, 0, 22, 36, 48, 43, 26, 51, 24, 73, 100, 68},
            //                     new[] {38, 78, 59, 97, 33, 71, 70, 23, 90, 53, 68, 45, 53, 103, 90, 15, 25, 67, 16, 22, 0, 54, 33, 59, 33, 31, 37, 93, 88, 84},
            //                     new[] {16, 24, 5, 44, 69, 20, 58, 41, 76, 75, 14, 47, 2, 67, 104, 57, 34, 16, 48, 36, 54, 0, 57, 7, 56, 72, 50, 57, 105, 61},
            //                     new[] {43, 69, 62, 83, 12, 61, 38, 53, 116, 83, 64, 76, 57, 113, 82, 48, 51, 72, 48, 48, 33, 57, 0, 57, 66, 18, 69, 113, 84, 115},
            //                     new[] {21, 18, 7, 38, 69, 13, 52, 48, 76, 80, 9, 53, 9, 65, 99, 64, 41, 15, 55, 43, 59, 7, 57, 0, 63, 74, 57, 57, 101, 61},
            //                     new[] {50, 75, 57, 84, 64, 75, 103, 19, 58, 24, 67, 15, 54, 70, 79, 19, 25, 59, 19, 26, 33, 56, 66, 63, 0, 59, 7, 61, 74, 52},
            //                     new[] {57, 88, 78, 100, 7, 80, 49, 53, 98, 69, 81, 73, 72, 109, 70, 41, 54, 88, 45, 51, 31, 72, 18, 74, 59, 0, 64, 117, 71, 107},
            //                     new[] {46, 68, 51, 77, 69, 68, 102, 17, 57, 27, 61, 9, 48, 67, 82, 23, 23, 52, 21, 24, 37, 50, 69, 57, 7, 64, 0, 57, 77, 48},
            //                     new[] {72, 44, 51, 31, 122, 52, 81, 70, 19, 49, 49, 49, 57, 12, 50, 80, 70, 42, 77, 73, 93, 57, 113, 57, 61, 117, 57, 0, 49, 11},
            //                     new[] {121, 83, 100, 63, 73, 90, 69, 92, 33, 51, 93, 77, 106, 39, 4, 81, 100, 90, 89, 100, 88, 105, 84, 101, 74, 71, 77, 49, 0, 48},
            //                     new[] {73, 52, 56, 42, 114, 60, 92, 63, 16, 39, 56, 40, 60, 19, 51, 70, 65, 47, 69, 68, 84, 61, 115, 61, 52, 107, 48, 11, 48, 0}
            //                 };
            //var nodeIds = new[]
            //              {
            //                  "Azores",
            //                  "Baghdad",
            //                  "Berlin",
            //                  "Bombay",
            //                  "Buenos Aires",
            //                  "Cairo",
            //                  "Capetown",
            //                  "Chicago",
            //                  "Guam",
            //                  "Honolulu",
            //                  "Istanbul",
            //                  "Juneau",
            //                  "London",
            //                  "Manila",
            //                  "Melbourne",
            //                  "Mexico City",
            //                  "Montreal",
            //                  "Moscow",
            //                  "New Orleans",
            //                  "New York",
            //                  "Panama City",
            //                  "Paris",
            //                  "Rio de Janeiro",
            //                  "Rome",
            //                  "San Francisco",
            //                  "Santiago",
            //                  "Seattle",
            //                  "Shanghai",
            //                  "Sydney",
            //                  "Tokyo"
            //              };
            var costMatrix = _128Cities.CostMatrix;
            var nodeIds = _128Cities.Names;

            var solver = new Solver((startIndex, endIndex) => costMatrix[startIndex,endIndex], 
                                    nodeIds,
                                    new Solver.SolverConfiguration()
                                    {
                                        EnableSolutionMutation = true,
                                        FirstSolutionStrategy = Solver.eFirstSolutionStrategy.ConnectCheapestArcs
                                    });
            solver.SetStartNode(nodeIds[0]);
            solver.SetEndNode(nodeIds[^1]);

            var calculateSolutionCost = new Func<int[], float>((solution) =>
                                                        {
                                                            float cost = 0f;
                                                            for (var i = 1; i < solution.Length; i++)
                                                            {
                                                                cost += costMatrix[solution[i - 1], solution[i]];
                                                            }
                                                            return cost;
                                                        });


            var resultCosts = new List<float>();
            var sw = Stopwatch.StartNew();
            var solution = new int[nodeIds.Length];

            foreach (var i in Enumerable.Range(0, 1))
            {
                var (_, solutionIndexes) = solver.FindSolution(200);
                var solutionCost = calculateSolutionCost(solutionIndexes);
                Debug.WriteLine($"RTsp Cost: {solutionCost}, elapsed: {Math.Round(sw.Elapsed.TotalMilliseconds, 2)}ms");
                resultCosts.Add(solutionCost);
                Array.Copy(solutionIndexes,0, solution, 0, solutionIndexes.Length);
                if (solutionIndexes.Distinct().Count() != nodeIds.Length)
                {
                    throw new Exception("błędna ścieżka");
                }
                sw.Restart();
            }

            resultCosts = resultCosts.OrderBy(x => x).ToList();
            RoutingIndexManager manager = new RoutingIndexManager(nodeIds.Length, 1, new []{0}, new []{nodeIds.Length-1});
            RoutingModel routing = new RoutingModel(manager);
            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) => {
                                                                           var fromNode = manager.IndexToNode(fromIndex);
                                                                           var toNode = manager.IndexToNode(toIndex);
                                                                           return costMatrix[fromNode, toNode];
                                                                       });

            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);
            RoutingSearchParameters searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.GlobalCheapestArc;

            var orToolsSolution = new int[nodeIds.Length];
            sw.Restart();
            Assignment sol = routing.SolveWithParameters(searchParameters);
            sw.Stop();
            var index = routing.Start(0);
            var step = 0;
            while (routing.IsEnd(index) == false)
            {
                orToolsSolution[step] = manager.IndexToNode((int)index);
                Console.Write($"{nodeIds[manager.IndexToNode((int)index)]} -> ");
                step++;
                index = sol.Value(routing.NextVar(index));
            }
            orToolsSolution[step] = manager.IndexToNode((int)index);
            var orToolsSolutionCost = calculateSolutionCost(orToolsSolution);
            Console.WriteLine(nodeIds[manager.IndexToNode((int)index)]);

            Debug.WriteLine($"OrTools Cost: {orToolsSolutionCost}, elapsed: {Math.Round(sw.Elapsed.TotalMilliseconds, 2)}ms");


            resultCosts = resultCosts.OrderBy(x=>x).ToList();
            Console.ReadKey();
        }
    }
}