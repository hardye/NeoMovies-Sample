using System;
using System.Collections.Generic;
using Neo4jClient;
using Neo4jClient.ApiModels.Cypher;
using Neo4jClient.Cypher;
using NeoMovies.Model.Nodes;
using NeoMovies.Model.Relationships;

namespace NeoMovies
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphClient client = GetClient();

            //GetAllTomHanksMovies(client);

            //GetAllCloudAtlasActors(client);

            //GetCloudAtlasRelations(client);

            //GetCloudAtlasActorsAndRoles(client);

            //GetHighestRatingMovies(client);

            //GetShortestPath(client);

            Console.ReadLine();
        }

        private static void GetAllTomHanksMovies(GraphClient client)
        {
            Console.WriteLine("*** Movies by Tom Hanks ***");

            // MATCH (:Person {name: "Tom Hanks"})-[:ACTED_IN]->(movie) RETURN movie

            const string actor = "Tom Hanks";

            var query = client.Cypher
                .Match("(:Person {name: {actorName}})-[:ACTED_IN]->(movie)")
                .WithParam("actorName", actor)
                .Return(movie => movie.As<Movie>())
                .OrderBy("movie.released");

            foreach (Movie movie in query.Results)
            {
                Console.WriteLine("{0} ({1})", movie.Title, movie.Released);
            }

            Console.WriteLine();
        }

        private static void GetAllCloudAtlasActors(GraphClient client)
        {
            Console.WriteLine("*** Actors of \"Cloud Atlas\" ***");

            // MATCH (:Movie {title: "Cloud Atlas"})<-[:ACTED_IN]->(person) RETURN person

            var query = client.Cypher
                .Match("(:Movie {title: \"Cloud Atlas\"})<-[:ACTED_IN]->(person)")
                .Return(person => person.As<Person>())
                .OrderBy("person.born");


            foreach (Person person in query.Results)
            {
                Console.WriteLine("{0} ({1})", person.Name, person.Born);
            }

            Console.WriteLine();
        }

        private static void GetCloudAtlasActorsAndRoles(GraphClient client)
        {
            Console.WriteLine("*** Actors and their roles in \"Cloud Atlas\" ***");

            // MATCH (person:Person)-[relatedTo:ACTED_IN]-(:Movie {title: "Cloud Atlas"}) RETURN person AS Person, relatedTo AS Relation

            var query = client.Cypher
                .Match("(person:Person)-[relatedTo:ACTED_IN]-(:Movie {title: \"Cloud Atlas\"})")
                .Return((person, relatedTo) => new
                {
                    Person = person.As<Person>(),
                    Relation = relatedTo.As<RelationshipInstance<ActedInProperties>>()
                });

            foreach (var result in query.Results)
            {
                Console.WriteLine("{0} ({1})", result.Person.Name, String.Join(", ", result.Relation.Data.Roles));
            }

            Console.WriteLine();
        }

        private static void GetCloudAtlasRelations(GraphClient client)
        {
            Console.WriteLine("*** How people are related to \"Cloud Atlas\" ***");

            // MATCH (person:Person)-[relatedTo]-(:Movie {title: "Cloud Atlas"}) RETURN person.name, type(relatedTo)
            // MATCH (person:Person)-[relatedTo]-(:Movie {title: "Cloud Atlas"}) RETURN person AS Person, type(relatedTo) AS RelationType

            const string movie = "Cloud Atlas";

            var query = client.Cypher
                .Match("(person:Person)-[relatedTo]-(:Movie {title: {movieTitle}})")
                .WithParam("movieTitle", movie)
                .Return((person, relatedTo) => new
                {
                    Person = person.As<Person>(),
                    RelationType = relatedTo.Type()
                });

            foreach (var result in query.Results)
            {
                Console.WriteLine("{0} ({1})", result.Person.Name, result.RelationType);
            }

            Console.WriteLine();
        }

        private static void GetHighestRatingMovies(GraphClient client)
        {
            Console.WriteLine("*** The 5 top-rated movies ***");

            // MATCH (movie:Movie)<-[review:REVIEWED]-(:Person) RETURN movie.title, review.rating ORDER BY review.rating DESC LIMIT 5

            var query = client.Cypher
                .Match("(movie:Movie)<-[review:REVIEWED]-(:Person)")
                .Return((movie, review) => new
                {
                    Movie = movie.As<Movie>(),
                    Review = review.As<RelationshipInstance<ReviewedProperties>>()
                })
                .OrderByDescending("review.rating")
                .Limit(5);

            foreach (var result in query.Results)
            {
                Console.WriteLine("Movie: '{0}', Review: '{1}', Rating: {2}", result.Movie.Title, result.Review.Data.Summary, result.Review.Data.Rating);
            }

            Console.WriteLine();
        }

        private static void GetShortestPath(GraphClient client)
        {
            // MATCH p=shortestPath((bacon:Person {name:"Kevin Bacon"})-[*]-(meg:Person {name:"Meg Ryan"})) RETURN p
            var query =
                client.Cypher
                    .Match("p=shortestPath((bacon:Person {name:\"Kevin Bacon\"})-[*]-(meg:Person {name:\"Meg Ryan\"}))")
                    .Return(p => new PathsResult
                    {
                        Nodes = Return.As<List<string>>("nodes(p)")
                    });

            foreach (PathsResult result in query.Results)
            {
                foreach (var node in result.Nodes)
                {
                    Console.WriteLine(node);
                }
            }

            Console.WriteLine();
        }

        private static GraphClient GetClient()
        {
            GraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            return client;
        }
    }
}
