/*
    public async Task<List<ItemModel>> FetchItemsAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["ItemsQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<Items>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        // Itt már csak a lista kell
        return json?.Data?.ItemsData ?? new List<ItemModel>();
    }


    public async Task<List<CategoryModel>> FetchCategoriesAsync()
    {
        var request = new GraphQLRequest
        {
            Query = queries["CategoriesQuery"]
        };

        var response = await _client.PostAsJsonAsync("", request);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"GraphQL HTTP error: {response.StatusCode}");

        var json = await response.Content.ReadFromJsonAsync<GraphQLResponse<Categories>>();

        if (json?.Errors != null && json.Errors.Any())
            throw new Exception($"GraphQL errors: {string.Join(';', json.Errors.Select(e => e.Message))}");

        return json?.Data?.ItemCategories ?? new List<CategoryModel>();
    }
*/