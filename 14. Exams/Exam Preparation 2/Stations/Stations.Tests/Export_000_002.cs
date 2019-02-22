﻿using System;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Stations.App;
using Stations.Data;
using Stations.Models;


[TestFixture]
public class Export_000_002
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<StationsDbContext>(
                options => options.UseInMemoryDatabase("Stations")
            );

        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile<StationsProfile>());

        this.serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void ExportCardsTicket()
    {
        var context = serviceProvider.GetService<StationsDbContext>();

        SeedDatabase(context);

        var expectedOutput = XDocument.Parse(@"<Cards><Card name=""George Powell"" type=""Debilitated""><Tickets><Ticket><OriginStation>Sofia</OriginStation><DestinationStation>Varna</DestinationStation><DepartureTime>24/05/2017 12:00</DepartureTime></Ticket><Ticket><OriginStation>Sofia</OriginStation><DestinationStation>Sagay</DestinationStation><DepartureTime>02/12/2016 12:20</DepartureTime></Ticket></Tickets></Card><Card name=""Henry Moreno"" type=""Debilitated""><Tickets><Ticket><OriginStation>Ajjah</OriginStation><DestinationStation>San Isidro</DestinationStation><DepartureTime>02/04/2016 12:33</DepartureTime></Ticket></Tickets></Card></Cards>");

        var actualOutput =
            XDocument.Parse(Stations.DataProcessor.Serializer.ExportCardsTicket(context, "Debilitated"));

        var xmlsAreEqual = XNode.DeepEquals(expectedOutput, actualOutput);
        Assert.That(xmlsAreEqual, Is.True, "ExportCardsTicket output is incorrect!");
    }

    private static void SeedDatabase(StationsDbContext context)
    {
        var stationsJson = @"[{""Name"":""Sofia"",""Town"":""Sofia""},{""Name"":""Sofia Sever"",""Town"":""Sofia""},{""Name"":""Davila"",""Town"":""Davila""},{""Name"":""Bulacan"",""Town"":""Bulacan""},{""Name"":""Monte de Trigo"",""Town"":""Taubate""},{""Name"":""Mos"",""Town"":""Mos""},{""Name"":""Bystre"",""Town"":""Bystre""},{""Name"":""Qinghe"",""Town"":""Caicara""},{""Name"":""Ninh Giang"",""Town"":""Klos""},{""Name"":""Vales"",""Town"":""Gazli""},{""Name"":""Chysky"",""Town"":""Bereza""},{""Name"":""Jaquimeyes"",""Town"":""Hongqi""},{""Name"":""San Isidro"",""Town"":""San Isidro""},{""Name"":""Pueblo Nuevo Vinas"",""Town"":""Balungkopi""},{""Name"":""Volzhsk"",""Town"":""Jambulang""},{""Name"":""Tunoshna"",""Town"":""Ervedosa do Douro""},{""Name"":""Kabare"",""Town"":""Yongan""},{""Name"":""Lapuz"",""Town"":""Ismailia""},{""Name"":""Huaian"",""Town"":""Pilar""},{""Name"":""Chateaubelair"",""Town"":""Chateaubelair""},{""Name"":""Ajjah"",""Town"":""Itapemirim""},{""Name"":""Belgrad"",""Town"":""Belgrad""},{""Name"":""Athene"",""Town"":""Athene""},{""Name"":""Vastra Frolunda"",""Town"":""Ajasse Ipo""},{""Name"":""San Juan de Colon"",""Town"":""San Juan de Colon""},{""Name"":""Kelungkung"",""Town"":""Renhe""},{""Name"":""Matriz de Camaragibe"",""Town"":""Nong Ruea""},{""Name"":""Cacapava"",""Town"":""Haarlem""},{""Name"":""Xishanzui"",""Town"":""Xishanzui""},{""Name"":""Masu"",""Town"":""Masu""},{""Name"":""Chandmani"",""Town"":""Marneuli""},{""Name"":""Sagay"",""Town"":""Gafargaon""},{""Name"":""Ovidiopol"",""Town"":""Tambalisa""},{""Name"":""Rzhev"",""Town"":""Onomichi""},{""Name"":""Varna"",""Town"":""Varna""},{""Name"":""Heqian"",""Town"":""Gaofeng""},{""Name"":""Hirvensalmi"",""Town"":""Xiwanzi""},{""Name"":""Cikabuyutan Barat"",""Town"":""Kuching""},{""Name"":""Ninh Hoa"",""Town"":""Ninh Hoa""},{""Name"":""Yinjiaxi"",""Town"":""Yinjiaxi""},{""Name"":""Balading"",""Town"":""Balading""},{""Name"":""Instanbul"",""Town"":""Instanbul""},{""Name"":""Bourgas"",""Town"":""Bourgas""}]";
        var stations = JsonConvert.DeserializeObject<Station[]>(stationsJson);
        context.Stations.AddRange(stations);

        var trainsJson = @"[{""TrainNumber"":""KB20012"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":50},{""SeatingClassId"":4,""Quantity"":44}]},{""TrainNumber"":""PM4956"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":3,""Quantity"":70},{""SeatingClassId"":4,""Quantity"":81}]},{""TrainNumber"":""IN035370"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":46},{""SeatingClassId"":1,""Quantity"":45},{""SeatingClassId"":3,""Quantity"":70}]},{""TrainNumber"":""BL436170"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":45},{""SeatingClassId"":3,""Quantity"":70},{""SeatingClassId"":4,""Quantity"":67}]},{""TrainNumber"":""TO371820"",""Type"":null,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":63}]},{""TrainNumber"":""UN2004411"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":58},{""SeatingClassId"":1,""Quantity"":11}]},{""TrainNumber"":""JN070789"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":41},{""SeatingClassId"":3,""Quantity"":36}]},{""TrainNumber"":""YG7183"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":3,""Quantity"":36},{""SeatingClassId"":4,""Quantity"":20},{""SeatingClassId"":1,""Quantity"":23}]},{""TrainNumber"":""CZ3766"",""Type"":null,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":20}]},{""TrainNumber"":""PU17333"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":58},{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":3,""Quantity"":45}]},{""TrainNumber"":""EA01424"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":58},{""SeatingClassId"":1,""Quantity"":23}]},{""TrainNumber"":""KU93940"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":58},{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":3,""Quantity"":45}]},{""TrainNumber"":""PS42970"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":3,""Quantity"":45}]},{""TrainNumber"":""KV372634"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":3,""Quantity"":45}]},{""TrainNumber"":""BQ877549"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":40},{""SeatingClassId"":2,""Quantity"":79}]},{""TrainNumber"":""HV212390"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":89},{""SeatingClassId"":3,""Quantity"":79}]},{""TrainNumber"":""MN9096"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":84},{""SeatingClassId"":4,""Quantity"":89}]},{""TrainNumber"":""CL7141"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":77},{""SeatingClassId"":3,""Quantity"":79}]},{""TrainNumber"":""QT6209"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":77},{""SeatingClassId"":3,""Quantity"":79}]},{""TrainNumber"":""NF0073"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":77},{""SeatingClassId"":3,""Quantity"":79}]},{""TrainNumber"":""IX00216"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":3,""Quantity"":62},{""SeatingClassId"":1,""Quantity"":47}]},{""TrainNumber"":""WK75971"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":4,""Quantity"":43}]},{""TrainNumber"":""QV395307"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":60},{""SeatingClassId"":1,""Quantity"":68}]},{""TrainNumber"":""AW8577"",""Type"":1,},{""TrainNumber"":""VT08003"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":46},{""SeatingClassId"":1,""Quantity"":45}]},{""TrainNumber"":""XM8366"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":45},{""SeatingClassId"":3,""Quantity"":70},{""SeatingClassId"":4,""Quantity"":67}]},{""TrainNumber"":""QN2714"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":88},{""SeatingClassId"":2,""Quantity"":51},{""SeatingClassId"":1,""Quantity"":11}]},{""TrainNumber"":""YP746803"",""Type"":null,},{""TrainNumber"":""FR46973"",""Type"":2,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":41},{""SeatingClassId"":3,""Quantity"":36}]},{""TrainNumber"":""KQ171903"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":20},{""SeatingClassId"":1,""Quantity"":23}]},{""TrainNumber"":""RW46969"",""Type"":0,},{""TrainNumber"":""AI8374"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":1,""Quantity"":23},{""SeatingClassId"":3,""Quantity"":45}]},{""TrainNumber"":""WQ598105"",""Type"":0,""TrainSeats"":[{""SeatingClassId"":4,""Quantity"":40},{""SeatingClassId"":2,""Quantity"":79}]},{""TrainNumber"":""KZ46189"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":69},{""SeatingClassId"":4,""Quantity"":23}]},{""TrainNumber"":""JX072057"",""Type"":1,""TrainSeats"":[{""SeatingClassId"":2,""Quantity"":84},{""SeatingClassId"":4,""Quantity"":89}]}]";
        var trains = JsonConvert.DeserializeObject<Train[]>(trainsJson);
        context.Trains.AddRange(trains);

        var cardsJson = @"[{""Name"":""John Levit"",""Age"":25,""Type"":4},{""Name"":""Todd Garcia"",""Age"":53,""Type"":3},{""Name"":""William Fox"",""Age"":115,""Type"":1},{""Name"":""Carlos Carroll"",""Age"":3,""Type"":1},{""Name"":""Katherine Olson"",""Age"":107,""Type"":4},{""Name"":""Eugene Duncan"",""Age"":1,""Type"":1},{""Name"":""Catherine Stanley"",""Age"":0,""Type"":0},{""Name"":""Joe Marshall"",""Age"":7,""Type"":3},{""Name"":""Joan Sain"",""Age"":1,""Type"":3},{""Name"":""Robert Hill"",""Age"":87,""Type"":1},{""Name"":""George Powell"",""Age"":105,""Type"":3},{""Name"":""Rose Morrison"",""Age"":112,""Type"":2},{""Name"":""Albert Roberts"",""Age"":11,""Type"":2},{""Name"":""Paul Boyd"",""Age"":47,""Type"":3},{""Name"":""Shirley Stewart"",""Age"":96,""Type"":1},{""Name"":""Juan Montgomery"",""Age"":117,""Type"":0},{""Name"":""Jack Alvarez"",""Age"":73,""Type"":1},{""Name"":""Carlos Lynch"",""Age"":43,""Type"":4},{""Name"":""Linda Stone"",""Age"":41,""Type"":0},{""Name"":""Gary Rivera"",""Age"":53,""Type"":2},{""Name"":""Sandra Cox"",""Age"":13,""Type"":4},{""Name"":""Keith Willis"",""Age"":75,""Type"":3},{""Name"":""Johnny Larson"",""Age"":49,""Type"":0},{""Name"":""Roger Nelson"",""Age"":22,""Type"":4},{""Name"":""Mary Ryan"",""Age"":108,""Type"":1},{""Name"":""Daniel Montgomery"",""Age"":31,""Type"":4},{""Name"":""Steve Wilson"",""Age"":19,""Type"":0},{""Name"":""David Carpenter"",""Age"":85,""Type"":4},{""Name"":""Kathleen Wells"",""Age"":54,""Type"":1},{""Name"":""Richard Kennedy"",""Age"":42,""Type"":0},{""Name"":""Richard Lynch"",""Age"":60,""Type"":4},{""Name"":""Louise Wheeler"",""Age"":21,""Type"":0},{""Name"":""Wanda Olson"",""Age"":35,""Type"":0},{""Name"":""Johnny Sanchez"",""Age"":3,""Type"":2},{""Name"":""Jeremy Carroll"",""Age"":115,""Type"":2},{""Name"":""Philip Gordon"",""Age"":104,""Type"":1},{""Name"":""Jacqueline Green"",""Age"":75,""Type"":0},{""Name"":""Rose Hicks"",""Age"":52,""Type"":3},{""Name"":""Lawrence Arnold"",""Age"":80,""Type"":0},{""Name"":""Keith Bryant"",""Age"":37,""Type"":0},{""Name"":""Rose Oliver"",""Age"":37,""Type"":3},{""Name"":""Robert Hansen"",""Age"":108,""Type"":1},{""Name"":""Roy George"",""Age"":23,""Type"":0},{""Name"":""Jane Meyer"",""Age"":79,""Type"":2},{""Name"":""Margaret Anderson"",""Age"":93,""Type"":1},{""Name"":""Wanda Ward"",""Age"":21,""Type"":2},{""Name"":""Marilyn Harvey"",""Age"":103,""Type"":2},{""Name"":""Matthew Gutierrez"",""Age"":47,""Type"":1},{""Name"":""Donna White"",""Age"":34,""Type"":4},{""Name"":""Anne Carr"",""Age"":115,""Type"":3},{""Name"":""Norma Carpenter"",""Age"":74,""Type"":1},{""Name"":""Bonnie Smith"",""Age"":97,""Type"":0},{""Name"":""Michelle Sanders"",""Age"":114,""Type"":2},{""Name"":""Sara Webb"",""Age"":49,""Type"":0},{""Name"":""Christine Watkins"",""Age"":104,""Type"":2},{""Name"":""Justin Hill"",""Age"":68,""Type"":2},{""Name"":""Marie Morales"",""Age"":6,""Type"":0},{""Name"":""Larry Palmer"",""Age"":102,""Type"":3},{""Name"":""Rebecca Ramirez"",""Age"":14,""Type"":3},{""Name"":""Jack Day"",""Age"":3,""Type"":1},{""Name"":""Henry Moreno"",""Age"":83,""Type"":3},{""Name"":""Diane Cruz"",""Age"":103,""Type"":2},{""Name"":""Margaret Harper"",""Age"":21,""Type"":1},{""Name"":""Joan Kennedy"",""Age"":11,""Type"":4},{""Name"":""Wanda Romero"",""Age"":29,""Type"":4},{""Name"":""Joan Henderson"",""Age"":36,""Type"":4},{""Name"":""Anna Young"",""Age"":90,""Type"":0},{""Name"":""Carl Cook"",""Age"":63,""Type"":2},{""Name"":""Sara Frazier"",""Age"":19,""Type"":1},{""Name"":""Johnny Daniels"",""Age"":24,""Type"":1},{""Name"":""Stephen Bennett"",""Age"":115,""Type"":2},{""Name"":""Ana Keanig"",""Age"":19,""Type"":1},{""Name"":""Melissa Oliver"",""Age"":84,""Type"":4},{""Name"":""Ann Hicks"",""Age"":108,""Type"":2},{""Name"":""Charles Harvey"",""Age"":13,""Type"":3},{""Name"":""Sara Watkins"",""Age"":48,""Type"":4},{""Name"":""Alan Albert"",""Age"":53,""Type"":4},{""Name"":""Lori Little"",""Age"":93,""Type"":3},{""Name"":""Beverly Long"",""Age"":23,""Type"":4},{""Name"":""Ernest Robertson"",""Age"":63,""Type"":4},{""Name"":""Patrick Roberts"",""Age"":19,""Type"":3},{""Name"":""Walter Knight"",""Age"":24,""Type"":0},{""Name"":""Helen Stephens"",""Age"":115,""Type"":4},{""Name"":""Carol Harper"",""Age"":50,""Type"":4},{""Name"":""Larry Hayes"",""Age"":13,""Type"":0},{""Name"":""Michelle Powell"",""Age"":57,""Type"":0},{""Name"":""Andrew Webb"",""Age"":22,""Type"":4},{""Name"":""Judy Morales"",""Age"":16,""Type"":1},{""Name"":""Theresa Fowler"",""Age"":17,""Type"":1},{""Name"":""Todd Castillo"",""Age"":52,""Type"":4},{""Name"":""Nicholas Greene"",""Age"":120,""Type"":2},{""Name"":""Evelyn Rogers"",""Age"":40,""Type"":4},{""Name"":""Denise Watson"",""Age"":118,""Type"":0},{""Name"":""Beverly Tucker"",""Age"":88,""Type"":1},{""Name"":""Carolyn Olson"",""Age"":79,""Type"":4},{""Name"":""Cheryl Griffin"",""Age"":113,""Type"":3},{""Name"":""Annie Rodriguez"",""Age"":42,""Type"":4},{""Name"":""Frances Harper"",""Age"":76,""Type"":3},{""Name"":""Evelyn Owens"",""Age"":79,""Type"":3}]";
        var cards = JsonConvert.DeserializeObject<CustomerCard[]>(cardsJson);
        context.Cards.AddRange(cards);

        var tripsJson = @"[{""DepartureTime"":""2016-12-27T12:00:00"",""ArrivalTime"":""2016-12-27T12:30:00"",""DestinationStationId"":2,""OriginStationId"":1,""Status"":0,""TrainId"":1},{""DepartureTime"":""2016-04-02T12:33:00"",""ArrivalTime"":""2016-04-10T23:11:00"",""DestinationStationId"":13,""OriginStationId"":21,""Status"":0,""TrainId"":4},{""DepartureTime"":""2016-09-17T14:34:00"",""ArrivalTime"":""2016-09-26T13:32:00"",""DestinationStationId"":1,""OriginStationId"":28,""Status"":1,""TrainId"":19},{""DepartureTime"":""2016-04-07T05:45:00"",""ArrivalTime"":""2016-04-14T07:59:00"",""DestinationStationId"":26,""OriginStationId"":28,""Status"":0,""TrainId"":4},{""DepartureTime"":""2016-03-22T06:54:00"",""ArrivalTime"":""2016-12-28T09:44:00"",""DestinationStationId"":17,""OriginStationId"":37,""Status"":1,""TrainId"":25},{""DepartureTime"":""2011-08-13T12:33:00"",""ArrivalTime"":""2016-08-15T09:55:00"",""DestinationStationId"":11,""OriginStationId"":19,""Status"":0,""TrainId"":10},{""DepartureTime"":""2017-02-01T14:33:00"",""ArrivalTime"":""2017-02-02T22:21:00"",""DestinationStationId"":35,""OriginStationId"":24,""Status"":1,""TrainId"":15},{""DepartureTime"":""2016-12-14T01:32:00"",""ArrivalTime"":""2016-12-14T23:59:00"",""DestinationStationId"":25,""OriginStationId"":11,""Status"":1,""TrainId"":3},{""DepartureTime"":""2016-05-18T00:00:00"",""ArrivalTime"":""2016-05-29T23:15:00"",""DestinationStationId"":9,""OriginStationId"":35,""Status"":0,""TrainId"":16},{""DepartureTime"":""2016-07-19T13:33:00"",""ArrivalTime"":""2016-07-23T21:33:00"",""DestinationStationId"":20,""OriginStationId"":34,""Status"":2,""TrainId"":22},{""DepartureTime"":""2016-05-24T13:30:00"",""ArrivalTime"":""2016-05-24T21:22:00"",""DestinationStationId"":18,""OriginStationId"":22,""Status"":0,""TrainId"":2},{""DepartureTime"":""2017-05-24T12:00:00"",""ArrivalTime"":""2017-05-24T22:30:00"",""DestinationStationId"":35,""OriginStationId"":1,""Status"":0,""TrainId"":1},{""DepartureTime"":""2016-12-02T12:20:00"",""ArrivalTime"":""2016-12-05T18:29:00"",""DestinationStationId"":32,""OriginStationId"":1,""Status"":1,""TrainId"":25},{""DepartureTime"":""2016-10-06T20:31:00"",""ArrivalTime"":""2016-10-14T22:11:00"",""DestinationStationId"":33,""OriginStationId"":4,""Status"":0,""TrainId"":10},{""DepartureTime"":""2016-12-28T13:44:00"",""ArrivalTime"":""2017-01-02T18:12:00"",""DestinationStationId"":11,""OriginStationId"":1,""Status"":1,""TrainId"":13},{""DepartureTime"":""2017-03-08T15:14:00"",""ArrivalTime"":""2017-03-17T18:31:00"",""DestinationStationId"":5,""OriginStationId"":35,""Status"":0,""TrainId"":23},{""DepartureTime"":""2017-02-19T14:33:00"",""ArrivalTime"":""2017-02-22T11:29:00"",""DestinationStationId"":3,""OriginStationId"":31,""Status"":0,""TrainId"":20},{""DepartureTime"":""2016-05-16T08:19:00"",""ArrivalTime"":""2016-05-21T17:33:00"",""DestinationStationId"":24,""OriginStationId"":6,""Status"":0,""TrainId"":21},{""DepartureTime"":""2016-12-27T07:33:00"",""ArrivalTime"":""2017-01-02T22:11:00"",""DestinationStationId"":30,""OriginStationId"":31,""Status"":0,""TrainId"":13},{""DepartureTime"":""2016-11-13T18:01:00"",""ArrivalTime"":""2016-11-15T12:12:00"",""DestinationStationId"":12,""OriginStationId"":36,""Status"":1,""TrainId"":23},{""DepartureTime"":""2016-04-19T15:13:00"",""ArrivalTime"":""2016-04-24T13:15:00"",""DestinationStationId"":12,""OriginStationId"":39,""Status"":0,""TrainId"":13},{""DepartureTime"":""2017-01-14T05:43:00"",""ArrivalTime"":""2017-01-19T03:33:00"",""DestinationStationId"":27,""OriginStationId"":1,""Status"":1,""TrainId"":11},{""DepartureTime"":""2017-05-24T22:00:00"",""ArrivalTime"":""2017-05-25T08:30:00"",""DestinationStationId"":35,""OriginStationId"":1,""Status"":0,""TrainId"":13},{""DepartureTime"":""2016-11-23T15:09:00"",""ArrivalTime"":""2016-11-26T16:00:00"",""DestinationStationId"":11,""OriginStationId"":27,""Status"":1,""TrainId"":11},{""DepartureTime"":""2016-10-07T03:22:00"",""ArrivalTime"":""2016-10-08T22:03:00"",""DestinationStationId"":42,""OriginStationId"":30,""Status"":0,""TrainId"":5},{""DepartureTime"":""2017-01-02T01:00:00"",""ArrivalTime"":""2017-01-07T01:30:00"",""DestinationStationId"":7,""OriginStationId"":20,""Status"":2,""TrainId"":4},{""DepartureTime"":""2016-08-23T14:30:00"",""ArrivalTime"":""2016-08-25T12:30:00"",""DestinationStationId"":27,""OriginStationId"":34,""Status"":0,""TrainId"":35},{""DepartureTime"":""2016-11-26T11:31:00"",""ArrivalTime"":""2016-11-27T12:43:00"",""DestinationStationId"":24,""OriginStationId"":33,""Status"":1,""TrainId"":17},{""DepartureTime"":""2016-03-21T12:33:00"",""ArrivalTime"":""2016-03-25T14:44:00"",""DestinationStationId"":21,""OriginStationId"":5,""Status"":0,""TrainId"":9},{""DepartureTime"":""2016-12-19T21:33:00"",""ArrivalTime"":""2016-12-22T12:00:00"",""DestinationStationId"":13,""OriginStationId"":27,""Status"":1,""TrainId"":7},{""DepartureTime"":""2017-02-16T11:31:00"",""ArrivalTime"":""2017-02-21T12:44:00"",""DestinationStationId"":21,""OriginStationId"":41,""Status"":0,""TrainId"":26},{""DepartureTime"":""2016-12-02T13:33:00"",""ArrivalTime"":""2016-12-11T18:00:00"",""DestinationStationId"":31,""OriginStationId"":2,""Status"":0,""TrainId"":2},{""DepartureTime"":""2016-03-21T12:45:00"",""ArrivalTime"":""2016-04-06T13:33:00"",""DestinationStationId"":7,""OriginStationId"":43,""Status"":1,""TrainId"":10},{""DepartureTime"":""2016-11-26T16:48:00"",""ArrivalTime"":""2016-12-01T01:33:00"",""DestinationStationId"":17,""OriginStationId"":27,""Status"":1,""TrainId"":10},{""DepartureTime"":""2017-01-01T07:41:00"",""ArrivalTime"":""2017-01-10T09:48:00"",""DestinationStationId"":13,""OriginStationId"":25,""Status"":1,""TrainId"":21},{""DepartureTime"":""2016-05-13T13:33:00"",""ArrivalTime"":""2016-05-22T22:48:00"",""DestinationStationId"":23,""OriginStationId"":38,""Status"":0,""TrainId"":3},{""DepartureTime"":""2017-01-02T14:55:00"",""ArrivalTime"":""2017-01-08T16:48:00"",""DestinationStationId"":24,""OriginStationId"":34,""Status"":0,""TrainId"":29},{""DepartureTime"":""2016-09-29T16:33:00"",""ArrivalTime"":""2016-10-03T22:42:00"",""DestinationStationId"":16,""OriginStationId"":28,""Status"":0,""TrainId"":6},{""DepartureTime"":""2017-02-01T23:22:00"",""ArrivalTime"":""2017-02-07T16:48:00"",""DestinationStationId"":9,""OriginStationId"":5,""Status"":1,""TrainId"":17},{""DepartureTime"":""2016-03-25T13:38:00"",""ArrivalTime"":""2016-03-28T19:39:00"",""DestinationStationId"":42,""OriginStationId"":14,""Status"":0,""TrainId"":35},{""DepartureTime"":""2016-07-13T16:19:00"",""ArrivalTime"":""2016-07-20T02:48:00"",""DestinationStationId"":19,""OriginStationId"":37,""Status"":0,""TrainId"":28},{""DepartureTime"":""2017-01-20T23:56:00"",""ArrivalTime"":""2017-02-03T04:45:00"",""DestinationStationId"":8,""OriginStationId"":30,""Status"":1,""TrainId"":29},{""DepartureTime"":""2016-07-20T16:48:00"",""ArrivalTime"":""2016-07-26T13:28:00"",""DestinationStationId"":12,""OriginStationId"":1,""Status"":1,""TrainId"":15},{""DepartureTime"":""2016-04-16T16:48:00"",""ArrivalTime"":""2016-04-22T17:48:00"",""DestinationStationId"":31,""OriginStationId"":35,""Status"":1,""TrainId"":1},{""DepartureTime"":""2016-07-03T09:00:00"",""ArrivalTime"":""2016-07-03T11:01:00"",""DestinationStationId"":14,""OriginStationId"":30,""Status"":0,""TrainId"":20},{""DepartureTime"":""2017-01-19T13:58:00"",""ArrivalTime"":""2017-01-24T17:18:00"",""DestinationStationId"":36,""OriginStationId"":15,""Status"":1,""TrainId"":9},{""DepartureTime"":""2016-05-24T14:30:00"",""ArrivalTime"":""2016-05-24T22:22:00"",""DestinationStationId"":18,""OriginStationId"":22,""Status"":0,""TrainId"":2}]";
        var trips = JsonConvert.DeserializeObject<Trip[]>(tripsJson);
        context.Trips.AddRange(trips);

        var ticketsJson = @"[{'Id':1,'CustomerCardId':66,'Trip':{'OriginStationId':34,'DestinationStationId':20,'DepartureTime':'2016-07-19T13:33:00'}},{'Id':4,'CustomerCardId':46,'Trip':{'OriginStationId':1,'DestinationStationId':2,'DepartureTime':'2016-12-27T12:00:00'}},{'Id':9,'CustomerCardId':49,'Trip':{'OriginStationId':27,'DestinationStationId':13,'DepartureTime':'2016-12-19T21:33:00'}},{'Id':12,'CustomerCardId':88,'Trip':{'OriginStationId':41,'DestinationStationId':21,'DepartureTime':'2017-02-16T11:31:00'}},{'Id':13,'CustomerCardId':52,'Trip':{'OriginStationId':4,'DestinationStationId':33,'DepartureTime':'2016-10-06T20:31:00'}},{'Id':14,'CustomerCardId':35,'Trip':{'OriginStationId':1,'DestinationStationId':35,'DepartureTime':'2017-05-24T22:00:00'}},{'Id':16,'CustomerCardId':35,'Trip':{'OriginStationId':19,'DestinationStationId':11,'DepartureTime':'2011-08-13T12:33:00'}},{'Id':17,'CustomerCardId':35,'Trip':{'OriginStationId':1,'DestinationStationId':35,'DepartureTime':'2017-05-24T12:00:00'}},{'Id':18,'CustomerCardId':35,'Trip':{'OriginStationId':1,'DestinationStationId':35,'DepartureTime':'2017-05-24T12:00:00'}},{'Id':23,'CustomerCardId':11,'Trip':{'OriginStationId':1,'DestinationStationId':35,'DepartureTime':'2017-05-24T12:00:00'}},{'Id':25,'CustomerCardId':11,'Trip':{'OriginStationId':1,'DestinationStationId':32,'DepartureTime':'2016-12-02T12:20:00'}},{'Id':27,'CustomerCardId':26,'Trip':{'OriginStationId':31,'DestinationStationId':3,'DepartureTime':'2017-02-19T14:33:00'}},{'Id':30,'CustomerCardId':45,'Trip':{'OriginStationId':28,'DestinationStationId':16,'DepartureTime':'2016-09-29T16:33:00'}},{'Id':32,'CustomerCardId':53,'Trip':{'OriginStationId':27,'DestinationStationId':11,'DepartureTime':'2016-11-23T15:09:00'}},{'Id':37,'CustomerCardId':69,'Trip':{'OriginStationId':25,'DestinationStationId':13,'DepartureTime':'2017-01-01T07:41:00'}},{'Id':42,'CustomerCardId':19,'Trip':{'OriginStationId':34,'DestinationStationId':27,'DepartureTime':'2016-08-23T14:30:00'}},{'Id':44,'CustomerCardId':23,'Trip':{'OriginStationId':33,'DestinationStationId':24,'DepartureTime':'2016-11-26T11:31:00'}},{'Id':47,'CustomerCardId':61,'Trip':{'OriginStationId':21,'DestinationStationId':13,'DepartureTime':'2016-04-02T12:33:00'}},{'Id':50,'CustomerCardId':28,'Trip':{'OriginStationId':39,'DestinationStationId':12,'DepartureTime':'2016-04-19T15:13:00'}}]";
        var tickets = JsonConvert.DeserializeObject<Ticket[]>(ticketsJson);

        context.Tickets.AddRange(tickets);

        context.SaveChanges();
    }
}