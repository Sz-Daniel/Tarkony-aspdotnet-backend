namespace GraphQL
{
   
    public class GraphQLException : Exception
    {
        public GraphQLException(string message)
            : base(message) { }

        public GraphQLException(string message, Exception inner)
            : base(message, inner) { }
    }

    public static class Query
    {
        public static readonly Dictionary<string, string> queries =
            new()
            {
                //Query = queries["ItemsQuery"];
                {
                    "APIStatusQuery",
                    @"query {
        status {
        currentStatuses { message name status statusCode }
        generalStatus { message name status statusCode }
        messages { content solveTime statusCode time type }
        }
        }"
                },
                {
                    "CategoriesQuery",
                    @"query{
        itemCategories {
        id
        name
        normalizedName
        children { normalizedName }
        parent { normalizedName }
        }
        }"
                },
                {
                    "ItemsQuery",
                    @" query {
            items {
                id
                name
                category {
                normalizedName
                }
                gridImageLink
                changeLast48h
                changeLast48hPercent
                normalizedName
                wikiLink
                sellFor {
                currency
                price
                priceRUB
                vendor {
                    name
                    ... on FleaMarket {
                    foundInRaidRequired
                    }
                }
                }
                buyFor {
                currency
                price
                priceRUB
                vendor {
                    name
                    ... on TraderOffer {
                    minTraderLevel
                    buyLimit
                    trader {
                        name
                        imageLink
                        levels {
                        level
                        requiredPlayerLevel
                        requiredReputation
                        requiredCommerce
                        }
                    }
                    taskUnlock {
                        name
                        minPlayerLevel
                    }
                    }
                }
                }
                bartersUsing {
                id
                level
                buyLimit
                taskUnlock {
                    name
                    minPlayerLevel
                }
                trader {
                    name
                    imageLink
                    levels {
                    level
                    requiredPlayerLevel
                    requiredReputation
                    requiredCommerce
                    }
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                bartersFor {
                id
                level
                buyLimit
                taskUnlock {
                    name
                    minPlayerLevel
                }
                trader {
                    name
                    imageLink
                    levels {
                    level
                    requiredPlayerLevel
                    requiredReputation
                    requiredCommerce
                    }
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                craftsUsing {
                id
                duration
                level
                station {
                    name
                    imageLink
                }
                taskUnlock {
                    name
                    minPlayerLevel
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                craftsFor {
                id
                duration
                level
                station {
                    name
                    imageLink
                }
                taskUnlock {
                    name
                    minPlayerLevel
                }
                rewardItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                requiredItems {
                    count
                    item {
                    id
                    gridImageLink
                    name
                    }
                }
                }
                usedInTasks {
                name
                objectives {
                    ... on TaskObjectiveItem {
                    description
                    count
                    item {
                        name
                    }
                    }
                }
                }
                receivedFromTasks {
                name
                finishRewards {
                    items {
                    count
                    item {
                        name
                    }
                    }
                }
                }
            }
            }"
                },
            };
    }
}
