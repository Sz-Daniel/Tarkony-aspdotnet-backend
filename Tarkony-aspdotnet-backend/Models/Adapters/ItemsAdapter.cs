using System;
using System.Collections.Generic;
using System.Linq;
using Items.Domain;
using Items.External;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.VisualBasic;

namespace Items.Adapter
{
    public static class ItemsAdapter
    {
        public static List<Domain.Items> ToDomain(List<External.ItemsModel> root)
        {
            return root.Select(ToDomain).ToList();
        }

        public static Domain.Items ToDomain(Items.External.ItemsModel src)
        {
            var sellList = src?.SellFor ?? new List<Items.External.Sell>();
            var buyList = src?.BuyFor ?? new List<Items.External.Buy>();

            return new Domain.Items
            {
                id = S(src?.Id),
                name = S(src?.Name),
                iconURL = S(src?.GridImageLink),
                bestSeller = BestSeller(sellList),
                bestBuy = BestBuy(buyList),
                changePrice = D(src?.ChangeLast48h),
                changePercent = D(src?.ChangeLast48hPercent),
                category = S(src?.Category?.NormalizedName),
                normalizedName = S(src?.NormalizedName),
                wiki = S(src?.WikiLink),
                sellTo = sellList.Select(MapSell).ToList(),
                buyFrom = buyList.Select(MapBuy).ToList(),
                barterInput = (src?.BartersUsing ?? new List<Items.External.Barter>())
                    .Select(MapBarter)
                    .ToList(),
                barterOutput = (src?.BartersFor ?? new List<Items.External.Barter>())
                    .Select(MapBarter)
                    .ToList(),
                craftInput = (src?.CraftsUsing ?? new List<Items.External.Craft>())
                    .Select(MapCraft)
                    .ToList(),
                craftOutput = (src?.CraftsFor ?? new List<Items.External.Craft>())
                    .Select(MapCraft)
                    .ToList(),
                taskNeed = MapTaskNeed(src?.UsedInTasks),
                taskGive = MapTaskGive(src?.UsedInTasks)
            };
        }

        private static SellTo MapSell(Items.External.Sell s) =>
            new SellTo
            {
                priceRub = (int)D(s?.PriceRUB),
                price = (int)D(s?.Price),
                priceCurrency = S(s?.Currency),
                fir = B(s?.Vendor?.FoundInRaidRequired),
                traderName = S(s?.Vendor?.Name)
            };

        private static BuyFrom MapBuy(Items.External.Buy b) =>
            new BuyFrom
            {
                priceRub = (int)D(b?.PriceRUB),
                price = (int)D(b?.Price),
                priceCurrency = S(b?.Currency),
                id = S(b?.Vendor?.Name),
                limit = 0,
                playertoTraderRequirements = MapTraderRequirements(b?.Trader),
                questRequirement = MapQuestRequirement(b?.TaskUnlock)
            };

        private static PriceDeal? BestSeller(List<Items.External.Sell> sellList)
        {
            var best = sellList
                .Where(s => s?.PriceRUB.HasValue == true)
                .OrderByDescending(s => s.PriceRUB.Value)
                .FirstOrDefault();

            return best == null
                ? null
                : new PriceDeal { price = (int)D(best.PriceRUB), place = S(best.Vendor?.Name) };
        }

        private static PriceDeal? BestBuy(List<Items.External.Buy> buyList)
        {
            var best = buyList
                .Where(b => b?.PriceRUB.HasValue == true)
                .OrderBy(b => b.PriceRUB.Value)
                .FirstOrDefault();

            return best == null
                ? null
                : new PriceDeal { price = (int)D(best.PriceRUB), place = S(best.Vendor?.Name) };
        }

        // --- Barter ---

        private static Domain.Barter MapBarter(Items.External.Barter b) =>
            new Domain.Barter
            {
                id = S(b?.Id),
                limit = I(b?.BuyLimit),
                PlayertoTraderRequirements = MapTraderRequirements(b?.Trader),
                QuestRequirement = MapQuestRequirement(b?.TaskUnlock),
                inputItems = (b?.RequiredItems ?? new List<Items.External.RequiredItem>())
                    .Select(MapCountedItem)
                    .ToList(),
                outputItems = (b?.RewardItems ?? new List<Items.External.RewardItem>())
                    .Select(MapCountedItem)
                    .ToList()
            };

        // --- Craft ---



        private static Domain.Craft MapCraft(Items.External.Craft c) =>
            new Domain.Craft
            {
                id = S(c?.Id),
                duration = I(c?.Duration),
                stationRequirement = MapStationRequirement(c?.Station, I(c?.Level)),
                questRequirement = MapQuestRequirement(c?.TaskUnlock),
                inputItems = (c?.RequiredItems ?? new List<Items.External.RequiredItem>())
                    .Select(MapCountedItem)
                    .ToList(),
                outputItems = (c?.RewardItems ?? new List<Items.External.RewardItem>())
                    .Select(MapCountedItem)
                    .ToList()
            };

        private static StationRequirement? MapStationRequirement(
            Items.External.Station s,
            int level
        ) =>
            s == null
                ? null
                : new StationRequirement
                {
                    level = level,
                    stationName = S(s.Name),
                    stationIcon = S(s.ImageLink)
                };

        // --- Requirements / Tasks ---

        private static PlayertoTraderRequirements MapTraderRequirements(
            Items.External.TraderInfo t
        ) =>
            new PlayertoTraderRequirements
            {
                traderName = S(t?.Name),
                traderIcon = S(t?.ImageLink),
                traderLevel =
                    t?.Levels != null && t.Levels.Count > 0 ? t.Levels.Max(l => l.Level) : 0,
                playerLevel =
                    t?.Levels != null && t.Levels.Count > 0
                        ? t.Levels.Max(l => l.RequiredPlayerLevel)
                        : 0,
                reputation =
                    t?.Levels != null && t.Levels.Count > 0
                        ? t.Levels.Max(l => l.RequiredReputation)
                        : 0.0,
                commerce =
                    t?.Levels != null && t.Levels.Count > 0
                        ? t.Levels.Max(l => l.RequiredCommerce)
                        : 0
            };

        private static QuestRequirement MapQuestRequirement(Items.External.TaskUnlock q) =>
            new QuestRequirement { level = I(q?.MinPlayerLevel), name = S(q?.Name) };

        private static ResponseCountedItem MapCountedItem(Items.External.RequiredItem ri) =>
            new ResponseCountedItem
            {
                count = (int)Math.Floor(D(ri?.Count)),
                id = S(ri?.Item?.Name), // No external id; using item name as identifier fallback
                img = "", // Not present in external; default empty
                name = S(ri?.Item?.Name)
            };

        private static ResponseCountedItem MapCountedItem(Items.External.RewardItem ri) =>
            new ResponseCountedItem
            {
                count = I(ri?.Count),
                id = S(ri?.Item?.Name),
                img = "",
                name = S(ri?.Item?.Name)
            };

        private static List<TaskNeed>? MapTaskNeed(List<Items.External.Task> tasks)
        {
            if (tasks == null || tasks.Count == 0)
                return null;

            return tasks
                .Select(
                    t =>
                        new TaskNeed
                        {
                            name = S(t?.Name),
                            task = (t?.Objectives ?? new List<Items.External.Objective>())
                                .Select(
                                    o =>
                                        new Domain.Task
                                        {
                                            description = S(o?.Description),
                                            name = S(o?.Item?.Name),
                                            count = I(o?.Count)
                                        }
                                )
                                .ToList()
                        }
                )
                .ToList();
        }

        private static List<TaskGive>? MapTaskGive(List<Items.External.Task> tasks)
        {
            if (tasks == null || tasks.Count == 0)
                return null;
            return tasks
                .Select(
                    t =>
                        new TaskGive
                        {
                            name = S(t?.Name),
                            reward = (t?.Objectives ?? new List<Items.External.Objective>())
                                .Select(
                                    o =>
                                        new TaskItem
                                        {
                                            name = S(o?.Item?.Name),
                                            count = I(o?.Count)
                                        }
                                )
                                .ToList()
                        }
                )
                .ToList();
        }

        private static string S(string? v) => v ?? string.Empty;

        private static int I(int? v) => v ?? 0;

        private static double D(double? v) => v ?? 0.0;

        private static bool B(bool? v) => v ?? false;
    }
}
