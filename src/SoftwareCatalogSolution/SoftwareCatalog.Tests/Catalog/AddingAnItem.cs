﻿
using Alba;
using SoftwareCatalog.Api.Catalog;

namespace SoftwareCatalog.Tests.Catalog;
public class AddingAnItem
{
    [Fact]
    public async Task CanAddAnItem()
    {
        var host = await AlbaHost.For<Program>();


        var itemToPost = new CatalogItemRequestModel
        {
            Name = "Visual Studio Code"
        };
        var expectedResponse = new CatalogItemResponseDetailsModel
        {
            Id = Guid.NewGuid(),
            Name = itemToPost.Name,
            Vendor = "Microsoft",
            Licence = CatalogItemLicenceTypes.OpenSource
        };
        var response = await host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl("/vendors/microsoft/opensource");
            api.StatusCodeShouldBe(201);
        });

        var body = response.ReadAsJson<CatalogItemResponseDetailsModel>();
        Assert.NotNull(body);

        Assert.Equal(expectedResponse, body);

    }
}
