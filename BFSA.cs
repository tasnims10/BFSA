using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } },
            { 3, new List<int> { 1, 4 } },
            { 4, new List<int> { 2, 3 } }
        };

        var influenceScores = ComputeInfluenceScores(graph);

        // Print the influence scores
        foreach (var kvp in influenceScores)
        {
            Console.WriteLine($"User {kvp.Key}: Influence Score = {kvp.Value}");
        }
    }

    static Dictionary<int, int> ComputeInfluenceScores(Dictionary<int, List<int>> graph)
    {
        //initializes an empty dictionary to store the results
        var influenceScores = new Dictionary<int, int>();
        //iterates over each node in the graph. each node acts as a starting point for a BFS to compute its influence score
        foreach (var user in graph.Keys)
        {
            // Perform BFS starting from the current user
            var distances = new Dictionary<int, int>(); // a dictionary stores the shortest distance from the current user to all other nodes
            var visited = new HashSet<int>();// a hash set to trach what has already been visited by BFS
            var queue = new Queue<int>(); // a queue to manage the nodes during BFS traversal

            // Initialize distances to infinity
            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;//initializing them to infinity to indicate they are initially unreacheable 
            }

            // Start BFS
            queue.Enqueue(user); // enqueues the current user into the BFS queue 
            distances[user] = 0; // sets the distance of the start node to itself as 0 
            visited.Add(user); //adds the start node to the visited set

            //processes the queue until its empty
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();// dequeues a node for processing 
                // iterates over all neighbours of the current node 
                foreach (var neighbor in graph[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor); // enqueues the neighbour into the queue
                        distances[neighbor] = distances[currentNode] + 1; // updates the distance for the neighbour as the current distance +1
                        visited.Add(neighbor); // marks the neighbour as visited
                    }
                }
            }

            // Calculate influence score for the current user
            int influenceScore = 0;
            foreach (var distance in distances.Values)
            {
                if (distance != int.MaxValue)
                {
                    influenceScore += distance; // sums all finite distances from the current user to all other nodes
                }
            }

            influenceScores[user] = influenceScore;// stores the computed influence score for the current user in the 'influencescores' dictionary
        }

        return influenceScores;// returns the dictionary containing the influence score for all users 
    }
}

