using System;
using Librum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librum.Controllers
{
    // [Authorize]
    [Route("article")]
    public class ArticleController : Controller
    {
        public ArticleController()
        {
        }
        
        [Authorize]
        [Route("new-post")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("new-post")]
        public IActionResult New(string article)
        {
            return View();
        }

        [AllowAnonymous]
        [Route("{slugArticle}")]
        public IActionResult Article(string slugArticle)
        {
            return View(new Article()
            {
                Title = "What does SafeBlock.io Offer ?",
                Description = "It was in 2017 that was born the idea of SafeBlock.io, our philosophy is quite simple, build a platform supplying many services and easy to use for novices we want both create innovative services that the blockchain community needs while making possible the cryptocurrencies usage in the real economy for people.",
                Keywords = "safebloc,cryptos,currency,cryptocurrencies,clint",
                IsDraft = false,
                Slug = "what-does-safeblock-io-offer",
                AuthorUsername = "clintm",
                WritedDatetime = DateTime.Parse("2019/04/01 20:30:00"),
                Content = @"# What does SafeBlock.io Offer ?

![](https://cdn-images-1.medium.com/max/800/1*eujMWgT83mDixNbLWT7QQQ.png)

It was in 2017 that was born the idea of SafeBlock.io, our philosophy is quite simple, build a platform supplying many services and easy to use for novices we want both create innovative services that the blockchain community needs while making possible the cryptocurrencies usage in the real economy for people.

It’s fair to note that currently the cryptocurrencies are mainly (and we hope for the shortest time possible) used by rich people to become richer and not really by the greatest number, but we think this trend will be reversed in few years, and we do our best to work on it.

## Overview of our Services

SafeBlock.io embed of course a secure wallet for $BTC, $ETH, $STRAT, $ZEC, $XMR, $LTC and $DASH (and more in the future), a mixing service, a low fees exchange, a simple way to buy coins and more explained below :

*    Vault: Store your funds in cryptos or in fiat.
*   Escrow Platform: Make secure transactions on the fly by using our API.
*    Full Node Hosting: Deploy a Full Node in few seconds and to secure your funds.
*    Cryptos Debit Card: Use your SafeBlock.io account for your everyday life.
*    API: Safely use our services everywhere.
*    And some surprises !

## Subscribe to our Newsletter

If you are interested in the project, and you want to get informed about the launching of SafeBlock.io, please, do not hesitate to click below and put your email address:

> [Subscribe to our Newsletter by Clicking Here](https://safeblock.io/)

## We Need your Help

We are currently hard working on SafeBlock to make the best product that fits our vision. We need talented people to reach the ship and navigate through the restless ocean of cryptocurrencies. If you want to contribute to make this ocean a better place to be, we need you so please consider joining us !
How to Join Us

*    Contact us by email at contact@safeblock.io
*    Join our Discord : https://discord.gg/HdsVDn2
*   Our twitter https://twitter.com/safeblock_io

## Share and Enjoy"
            });
        }
    }
}