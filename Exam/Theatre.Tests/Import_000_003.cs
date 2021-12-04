//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Theatre;
using Theatre.Data;
using Theatre.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[TestFixture]
public class Import_000_003
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile(GetType("TheatreProfile")));

        this.serviceProvider = ConfigureServices<TheatreContext>("Theater");
    }

    [Test]
    public void ImportCastsZeroTest()
    {
        var context = this.serviceProvider.GetService<TheatreContext>();
             
        var inputXml =
            @"<?xml version='1.0' encoding='UTF-8'?>
<Casts>
  <Cast>
    <FullName>Van Tyson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-35-745-2774</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Carlina Desporte</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-00-715-9959</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Elke Kavanagh</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-468-3479</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Lorry Ferreo</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-03-229-7456</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Vonny Henlon</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-29-590-5125</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Brock Palle</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-458-2012</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Jefferson Chell</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-02-183-3699</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Estelle Haycox</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-22-799-4279</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Torrin Darke</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-23-069-4342</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Andie Greatham</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-87-646-3735</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Galvin Iggulden</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-26-507-6901</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Currey Le Frank</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-81-800-1289</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Ernaline Gayforth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-01-748-4377</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Devy Everest</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-665-5842</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Ashly Manchett</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-96-124-0972</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Donnie Stonard</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-95-484-0739</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Katie Marryatt</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-12-967-3484</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Caddric Beasley</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-69-332-4316</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-10-514-3751</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Royal Dunster</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-98-548-8778</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Ashli Thurstance</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-46-263-7913</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Germain Makinson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-65-137-1171</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Thaddeus Kemer</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-36-117-5701+44-36-117-5701</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Freddy Tuckett</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-82-161-8801</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Jessy Andriulis</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-92-844-2109</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Heidie Pudsey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-32-669-6265</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Artemis Stable</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-11-687-3146</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-66-985-5588</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Ag Ewbanche</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-45-166-6135</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Nerti Ridel</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-54-951-1765</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Shepperd Girke</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-55-984-1459</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Silvana Flegg</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-32-960-2798</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Cary Wolstenholme</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-62-048-9741</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Westley Harmes</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-35-0313</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName>Salaidh Dedon</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-70-074-0976</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Clemence Pattemore</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-48-272-1144</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Sella Mains</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-11-336-0569</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Whitaker Emson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-56-662-6173</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Sigismondo Pettiford</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-77-111-9152</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Anastassia Copcott</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-73-555-2945</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Irving Houlridge</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-93-062-5418</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Alfi Grasser</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-60-381-3560</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Zorana Kitcat</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-90-723-7850</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Buckie MacDuff</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-88-16</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Wilma Whitelock</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-74-126-3901</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Dorothea Jest</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-97-118-2800</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Carri Moroney</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-63-091-8047</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Curr Bedburrow</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-02-643-6334</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Darryl Shobbrook</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-61-608-6855</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Reinold Paddemore</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-04-413-5198</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Karlene Vasyutochkin</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-10-248-6163</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Peggie Bowring</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-36-729-3666</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-56-398-7421</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Alvinia Jachimczak</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-25-593-3596</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Gabbey Peert</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-76-582-5952</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Anastasie McNally</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-20-095-8776</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-79-133-5028</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Monroe Spraggon</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-91-822-1676</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Jacinta Robertshaw</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-49-804-7360</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Hussein Eldridge</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-59-364-4119</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-63-396-5095</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Myrtice Leimster</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-73-625-1869</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Corabelle Frankcomb</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-70-609-8548</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Aprilette Clemmett</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-18-068-8886</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Bartholomeus Pentecust</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-16-223-0746</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Mannie Plomer</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-68-780-7846</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Marni Sellack</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-62-868-2269</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Jamey Medgwick</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-75-974-1627</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Greer Croyden</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-69-537-5478</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-81-224-9561</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-58-614-7723</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Wren O'Noland</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-77-140-7112</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Sergei Pendry</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-93-721-0812</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Alphonso Fulcher</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-20-793-2683</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Venita Dronsfield</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-02-674-8440</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Cristine Van Brug</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-39-677-9231</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Gerianna Gianuzzi</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-49-698-5777</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Traci Burgis</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-08-776-8059</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Franciskus Burress</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-12-220-5868</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Brod Doy</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-95-665-1270</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-98-960-7561</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Maddalena Masterson</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-65-043-0838</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Harrietta Hoofe</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-63-782-4570</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Hanson Tutchener</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-94-148-2043</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Gustaf Weadick</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-76-063-1476</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Xenia Osipov</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-82-002-2692</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Vally Baunton</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-37-620-9595</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Hollyanne Domerque</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-58-535-1566</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Debra Leeder</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-73-489-7059</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Gail Avieson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-99-090-0206</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Rafaello Licari</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-85-141-4057</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Fonsie Soughton</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-03-887-5847</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Ganny Kenrick</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-07-225-1283</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-24-919-2351</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Henka Padberry</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-99-009-9601</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Claybourne Bathersby</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-34-4569</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Celine Kerwick</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-30-794-3989</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Farleigh Pascho</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-19-194-6069</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Jennette Neiland</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-671-0777</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Dorian Wickins</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-46-836-9912</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-43-500-1295</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Lurlene Swaffer</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-77-183-7497</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Kay Illing</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-90-798-2238</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Janette Gascar</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-81-953-8972</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Rosamund Willoughey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>00-195+44-</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Chet Carloni</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-54-848-3285</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-46-242-3656</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-21-227-4849</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Flora Greenaway</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-35-137-4185</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Bendick Trorey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-46-891-6111</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Jocelyne Dartnell</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-02-718-0205</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Kessia Oliphant</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-44-516-3998</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Roderick Kettlestringe</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-33-444-1761</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Waylan Durand</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-33-370-5218</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Vittorio Baise</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-35-076-5832</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Wanda Commings</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-13-323-8461</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Chris Lindley</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-47-494-0852</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Carce Nuth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-34-394-3038</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Sela Hillett</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-29-534-3301</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Tristam Goneau</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-51-5556</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Jude Jeaneau</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-45-826-7179</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Edmon Busst</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-01-255-5484</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Aubrey Syms</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-87-293-5336</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-86-403-5172</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName>Kitti Leisman</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-06-417-6369</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-47-999-0565</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Filide Wicks</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-33-558-0340</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Ainslie Scollan</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-31-617-9133</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-80-116-0175</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Vincenty Dodsworth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-49-653-9811</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Rochette Polleye</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-39-182-1189</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Zacherie Rainforth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-94-409-8755</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Madella Van Son</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-84-084-1742</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Troy Lawlee</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-18-929-2098</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Cheri Lashley</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-26-757-7198</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Lissa Pummery</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-49-567-1701</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Adoree Boothby</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-70-816-4503</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>El</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-468-3479</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Maurizia Artus</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-36-239-8352</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-07-531-1819</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Carmon Pirdy</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-75-231-6370</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Charleen Blemings</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-17-877-0455</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Aubert Shama</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-59-378-8491</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-86-874-7616</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Waring Ducaen</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-78-711-1009</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Joelle Pinkstone</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-65-538-1323</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Yettie Copping</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-21-892-0766</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Wes Thoumasson</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-19-272-9963</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName>Clerissa Fellgate</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-20-722-3800</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Durand Rapper</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-21-868-0543</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Celene Whelpdale</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-10-084-3212</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Bryna Alti</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-18-528-0717</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Munroe Bringloe</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-92-289-4891</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-785-2727</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Agatha Tomashov</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-60-930-9138</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Ari Huriche</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-72-876-7469</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Carlyle Rack</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-41-594-5017</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-42-004-6432</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Abbe Bage</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-60-428-2538</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Aindrea Bellwood</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-43-133-6726</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Caye Blacklawe</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-20-005-7399</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Renault Kevlin</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-48-403-0818</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Marcello Brothers</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-97-927-2639</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName>Aurore Blazej</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-60-115-7376</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Torin Rylett</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-65-887-2839</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Sarita Velde</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-89-790-6526</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Donall Haggett</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-39-737-9792</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Annabel Elvidge</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-73-322-9969</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-48-915-1003</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Loutitia Joy</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-51-023-2163</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+4-27-847-4578</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Amy Haskell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-63-489-3145</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Elyse Innocenti</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-28-486-8670</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Briney Hazel</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-77-138-0604</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Tyson Antonellini</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-72-870-4149</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Athene Blaylock</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-82-479-7070</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Ted Charley</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-04-404-4958</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Pammie Siege</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-92-075-9264</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Kellina Daingerfield</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-53-864-5237</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Celesta McGillreich</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>09-144-0884</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Franny Hopewell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-55-305-0304</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Gregoire Hardan</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-86-100-4079</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Janka Atkin</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-06-729-6631</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-04-911-0199</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Isadore Renol</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-51-641-5758</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Jemimah Biggadyke</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-68-549-2898</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Gay Sabie</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-37-877-9110</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Eba Klemt</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-0029</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Atlante Danilchenko</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>00-058-2809</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Rory Jarad</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-70-783-6464</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Hildagarde Itzkowicz</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-40-365-9276</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Corny Pegrum</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-57-277-1053</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Alica O'Hanley</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-77-999-2366</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Legra Strong</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-81-755-7351</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Arel Corps</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-67-558-0250</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Beauregard Hagyard</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-33-559-2088</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Nannie Embra</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-04-018-3797</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-96-860-3374</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Sylvia Felipe</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-76-967-5545</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Dorrie Winstanley</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-25-885-3103</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-91-955-7567</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Rycca Klimkov</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-52-436-3380</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Tobin Tamburi</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-10-327-6959</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName>Alexine Rickhuss</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-14-016-1637</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Si Grigoryev</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-10-197-9440</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Daloris Cornewell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-03-949-2497</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Louie Sherratt</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-55-719-1603</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Marcile Donke</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-73-668-3153</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Christian Geere</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-36-346-7859</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName>Rafaelita Karp</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-27-949-7349</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Dedie Northcote</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-80-576-6593</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Donavon Polhill</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-048-3535</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Loria Gowdy</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-00-731-4388</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Virgilio Kunat</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-72-224-0625</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Alair Cattonnet</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-00-026-1334</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Linnea Edgeley</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-18-930-1592</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Shep Eck</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-11-254-2347</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Renell Rosenbush</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-25-240-2550</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Bobbe Service</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-73-134-9341</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Ellie Emtage</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-88-288-2074</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Evita Ridler</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-115-1270</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Smitty Beaty</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-50-121-6518</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Delphine Crone</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-61-908-3689</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Perice Ricker</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-84-025</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-99-308-6639</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Cindelyn Mance</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-66-577-4267</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Maisie Hymas</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-166-2028</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Steven Lanfer</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-91-825-4824</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Joseito Melmoth</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-28-975-5145</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Robbert Tuvey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-09-621-7281</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Rodney O'Neill</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-39-554-7223</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Cosetta Mauditt</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-48-104-7102</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Pippy Ennever</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-36-374-9393</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Clarie Ethelston</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-44-624-5803</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Shelby Luety</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-30-298-8506</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Edouard Idle</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-39-001-7562</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Abbi Sandwich</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-01-258-1156</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Ingram Raybould</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-64-948-1649</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-55-894-0454</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Maryjane Rikard</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-78-688-6321</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Miles Kantor</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-25-658-9332</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Trever Hulburd</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-05-614-2696</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Corby Tonnesen</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+43-71-261-2716</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Isahella Rakestraw</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-45-082-1456</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Matthew McFetrich</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-49-146-9670</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Ennis Fisbey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-914-9522</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Carina Donnison</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-59-297-6968</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Odilia Jasiak</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-55-502-7636</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName>Carley Lomansey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-97-256-5825</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-40-262-5077</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Nance Pollie</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-99-608-4851</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName>Chilton D'Ambrogi</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-76-158-9650</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName>Marquita Brugger</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-96-293-2841</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Milo Kemp</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-74-014-2322</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Glad Fucher</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>24-+44-4866</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Isis Bessey</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-52-566-8095</PhoneNumber>
    <PlayId>24</PlayId>
  </Cast>
  <Cast>
    <FullName>Colet Biernat</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-26-435-2679</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-89-460-5165</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Emily Crawforth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-96-827-9049</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Tricia Lantry</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-17-175-1497</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Branden Drewe</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-31-130-8355</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Barron Atwill</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-79-122-0273</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Ardis Fellnee</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-03-086-0587</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Adolf Gally</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-55-483-6686</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Andriana Joskowicz</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-88-235-4961</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Averil Habbert</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-38-085-5313</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Welsh Bomb</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-43-905-4845</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>van</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-468-3479</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Kenny Shegog</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-72-542-4997</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Mabel Belfit</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-80-950-3094</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Salli Gierck</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-60-103-3340</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Nessie Dodgshun</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-44-464-2223</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Corina Wilce</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-64-750-3817</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Petronia Powter</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-76-628-4029</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Leupold Prenty</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-52-337-7345</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-73-462-4927</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Raymund Odlin</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-43-716-1794</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Cornie Schottli</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-99-987-4502</PhoneNumber>
    <PlayId>16</PlayId>
  </Cast>
  <Cast>
    <FullName>Wilmar Ashwell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-54-975-6796</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Gardener Josselsohn</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-74-176-2689</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Sybilla Aspin</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-59-055-4128</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-72-651-0169</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-19-596-8328</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Lorraine Thorwarth</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-95-859-2372</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-72-437-7200</PhoneNumber>
    <PlayId>2</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-86-263-1739</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-59-729-2164</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Hanson Jirus</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>47-990-9144</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName>Oralle Bladder</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-19-754-2324</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Wylma Tillerton</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-83-198-5838</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Theodosia Carlton</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-86-840-9962</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-65-815-9190</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Grover Petherick</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-86-026-6212</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Elsworth Francioli</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-29-528-2588</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Esteban Tander</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-20-630-3142</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>E</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-468-3479</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Ingaborg Oakenfield</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-81-152-4755</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-24-116-1190</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Webster Filson</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-06-820-5587</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Ingram Todarini</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-415-1701</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName>Mattie GiacobbiniJacob</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-44-221-5335</PhoneNumber>
    <PlayId>3</PlayId>
  </Cast>
  <Cast>
    <FullName>Whitaker Ozelton</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-59-576-9710</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Melisandra Tranckle</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-98-539-2982</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Elisabeth Abdey</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>56-+44--6304</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Shirlene Canedo</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-90-611-8710</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Decca Barles</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-92-759-0188</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Rhianna McLurg</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-03-471-4933</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Livia Skillen</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-83-280-3097</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Rubina Woolard</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-88-337-8352</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Iolanthe Wharrier</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-02-864-5651</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Rubi Baptist</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-48-985-3966</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Elwyn Callister</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-64-223-7226</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Jayme Bakes</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-91-656-5436</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>nh</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-53-468-3479</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName>Hersh Birdis</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-91-842-6054</PhoneNumber>
    <PlayId>4</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-59-742-3119</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-98-315-9794</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Tori Lovick</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-378-8470</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Nathalie Neiland</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-57-097-8331</PhoneNumber>
    <PlayId>30</PlayId>
  </Cast>
  <Cast>
    <FullName>Clarita Quarterman</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-68-220-4196</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Jaquith Kohter</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-34-746-0493</PhoneNumber>
    <PlayId>10</PlayId>
  </Cast>
  <Cast>
    <FullName>Thadeus Filgate</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-78-052-6211</PhoneNumber>
    <PlayId>11</PlayId>
  </Cast>
  <Cast>
    <FullName>Jeane Freen</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-02-230-7788</PhoneNumber>
    <PlayId>7</PlayId>
  </Cast>
  <Cast>
    <FullName>Wenona Collumbell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-04-050-6349</PhoneNumber>
    <PlayId>23</PlayId>
  </Cast>
  <Cast>
    <FullName>Monro Cord</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-09-780-5092</PhoneNumber>
    <PlayId>20</PlayId>
  </Cast>
  <Cast>
    <FullName>Sigrid Rhoddie</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-94-558-7776</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Engelbert Maharg</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-52-808-8590</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Alena Garretson</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-88-004</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName>Mariann Speerman</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-92-352-5106</PhoneNumber>
    <PlayId>22</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-41-991-1551</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName>Antony Kempe</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-50-634-5821</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Reamonn Maleby</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-93-143-9362</PhoneNumber>
    <PlayId>9</PlayId>
  </Cast>
  <Cast>
    <FullName>Emelda Yule</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-88-521-9542</PhoneNumber>
    <PlayId>28</PlayId>
  </Cast>
  <Cast>
    <FullName>Nick Folkes</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>04-349-288+44-2</PhoneNumber>
    <PlayId>25</PlayId>
  </Cast>
  <Cast>
    <FullName>Aura Wauchope</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-11-780-5904</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-74-941-0246</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Codie Demaine</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-82-564-9976</PhoneNumber>
    <PlayId>6</PlayId>
  </Cast>
  <Cast>
    <FullName>Sarine Tidgewell</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-79-230-6734</PhoneNumber>
    <PlayId>29</PlayId>
  </Cast>
  <Cast>
    <FullName>Otho Nesby</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>60-905+44--0098</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Keir Copnall</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-34-318-2734</PhoneNumber>
    <PlayId>21</PlayId>
  </Cast>
  <Cast>
    <FullName>Giacomo Belward</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-80-462-0036</PhoneNumber>
    <PlayId>27</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-09-914-9724</PhoneNumber>
    <PlayId>12</PlayId>
  </Cast>
  <Cast>
    <FullName>Clement Pykett</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-95-331-3013</PhoneNumber>
    <PlayId>18</PlayId>
  </Cast>
  <Cast>
    <FullName>Solomon Pinwill</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-88-717-2780</PhoneNumber>
    <PlayId>5</PlayId>
  </Cast>
  <Cast>
    <FullName>Shermy Iskow</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-98-463-1986</PhoneNumber>
    <PlayId>15</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-38-594-0623</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Emmott Ditts</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-35-497-0732</PhoneNumber>
    <PlayId>13</PlayId>
  </Cast>
  <Cast>
    <FullName>Hatty Friary</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-20-352-2388</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName>Zonnya Miner</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-87-503-2640</PhoneNumber>
    <PlayId>8</PlayId>
  </Cast>
  <Cast>
    <FullName/>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-67-792-9706</PhoneNumber>
    <PlayId>14</PlayId>
  </Cast>
  <Cast>
    <FullName>Adelaida Hadlow</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-35-737-4249</PhoneNumber>
    <PlayId>17</PlayId>
  </Cast>
  <Cast>
    <FullName>Tarrah Scouler</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-80-377-0406</PhoneNumber>
    <PlayId>19</PlayId>
  </Cast>
  <Cast>
    <FullName>Morganica Irons</FullName>
    <IsMainCharacter>false</IsMainCharacter>
    <PhoneNumber>+44-91-230-8220</PhoneNumber>
    <PlayId>1</PlayId>
  </Cast>
  <Cast>
    <FullName>Whitney Standering</FullName>
    <IsMainCharacter>true</IsMainCharacter>
    <PhoneNumber>+44-16-828-7732</PhoneNumber>
    <PlayId>26</PlayId>
  </Cast>
</Casts>";
        ;
        var actualOutput =
            Theatre.DataProcessor.Deserializer.ImportCasts(context, inputXml).TrimEnd();
        ;
        var expectedOutput =
            "Successfully imported actor Van Tyson as a lesser character!\r\nSuccessfully imported actor Carlina Desporte as a lesser character!\r\nSuccessfully imported actor Elke Kavanagh as a main character!\r\nSuccessfully imported actor Lorry Ferreo as a lesser character!\r\nSuccessfully imported actor Vonny Henlon as a main character!\r\nSuccessfully imported actor Brock Palle as a main character!\r\nSuccessfully imported actor Jefferson Chell as a main character!\r\nSuccessfully imported actor Estelle Haycox as a main character!\r\nSuccessfully imported actor Torrin Darke as a main character!\r\nSuccessfully imported actor Andie Greatham as a main character!\r\nSuccessfully imported actor Galvin Iggulden as a lesser character!\r\nSuccessfully imported actor Currey Le Frank as a main character!\r\nSuccessfully imported actor Ernaline Gayforth as a main character!\r\nSuccessfully imported actor Devy Everest as a main character!\r\nSuccessfully imported actor Ashly Manchett as a main character!\r\nSuccessfully imported actor Donnie Stonard as a main character!\r\nSuccessfully imported actor Katie Marryatt as a main character!\r\nSuccessfully imported actor Caddric Beasley as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Royal Dunster as a main character!\r\nSuccessfully imported actor Ashli Thurstance as a main character!\r\nSuccessfully imported actor Germain Makinson as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Freddy Tuckett as a lesser character!\r\nSuccessfully imported actor Jessy Andriulis as a lesser character!\r\nSuccessfully imported actor Heidie Pudsey as a main character!\r\nSuccessfully imported actor Artemis Stable as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Ag Ewbanche as a lesser character!\r\nSuccessfully imported actor Nerti Ridel as a main character!\r\nSuccessfully imported actor Shepperd Girke as a lesser character!\r\nSuccessfully imported actor Silvana Flegg as a main character!\r\nSuccessfully imported actor Cary Wolstenholme as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Salaidh Dedon as a main character!\r\nSuccessfully imported actor Clemence Pattemore as a main character!\r\nSuccessfully imported actor Sella Mains as a main character!\r\nSuccessfully imported actor Whitaker Emson as a lesser character!\r\nSuccessfully imported actor Sigismondo Pettiford as a main character!\r\nSuccessfully imported actor Anastassia Copcott as a lesser character!\r\nSuccessfully imported actor Irving Houlridge as a main character!\r\nSuccessfully imported actor Alfi Grasser as a main character!\r\nSuccessfully imported actor Zorana Kitcat as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Wilma Whitelock as a lesser character!\r\nSuccessfully imported actor Dorothea Jest as a lesser character!\r\nSuccessfully imported actor Carri Moroney as a main character!\r\nSuccessfully imported actor Curr Bedburrow as a lesser character!\r\nSuccessfully imported actor Darryl Shobbrook as a main character!\r\nSuccessfully imported actor Reinold Paddemore as a main character!\r\nSuccessfully imported actor Karlene Vasyutochkin as a main character!\r\nSuccessfully imported actor Peggie Bowring as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Alvinia Jachimczak as a lesser character!\r\nSuccessfully imported actor Gabbey Peert as a lesser character!\r\nSuccessfully imported actor Anastasie McNally as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Monroe Spraggon as a main character!\r\nSuccessfully imported actor Jacinta Robertshaw as a lesser character!\r\nSuccessfully imported actor Hussein Eldridge as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Myrtice Leimster as a main character!\r\nSuccessfully imported actor Corabelle Frankcomb as a main character!\r\nSuccessfully imported actor Aprilette Clemmett as a lesser character!\r\nSuccessfully imported actor Bartholomeus Pentecust as a main character!\r\nSuccessfully imported actor Mannie Plomer as a main character!\r\nSuccessfully imported actor Marni Sellack as a main character!\r\nSuccessfully imported actor Jamey Medgwick as a main character!\r\nSuccessfully imported actor Greer Croyden as a lesser character!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Wren O'Noland as a main character!\r\nSuccessfully imported actor Sergei Pendry as a lesser character!\r\nSuccessfully imported actor Alphonso Fulcher as a main character!\r\nSuccessfully imported actor Venita Dronsfield as a lesser character!\r\nSuccessfully imported actor Cristine Van Brug as a main character!\r\nSuccessfully imported actor Gerianna Gianuzzi as a lesser character!\r\nSuccessfully imported actor Traci Burgis as a main character!\r\nSuccessfully imported actor Franciskus Burress as a lesser character!\r\nSuccessfully imported actor Brod Doy as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Maddalena Masterson as a main character!\r\nSuccessfully imported actor Harrietta Hoofe as a lesser character!\r\nSuccessfully imported actor Hanson Tutchener as a main character!\r\nSuccessfully imported actor Gustaf Weadick as a lesser character!\r\nSuccessfully imported actor Xenia Osipov as a main character!\r\nSuccessfully imported actor Vally Baunton as a lesser character!\r\nSuccessfully imported actor Hollyanne Domerque as a lesser character!\r\nSuccessfully imported actor Debra Leeder as a main character!\r\nSuccessfully imported actor Gail Avieson as a lesser character!\r\nSuccessfully imported actor Rafaello Licari as a main character!\r\nSuccessfully imported actor Fonsie Soughton as a main character!\r\nSuccessfully imported actor Ganny Kenrick as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Henka Padberry as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Celine Kerwick as a main character!\r\nSuccessfully imported actor Farleigh Pascho as a main character!\r\nSuccessfully imported actor Jennette Neiland as a main character!\r\nSuccessfully imported actor Dorian Wickins as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Lurlene Swaffer as a lesser character!\r\nSuccessfully imported actor Kay Illing as a lesser character!\r\nSuccessfully imported actor Janette Gascar as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Chet Carloni as a lesser character!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Flora Greenaway as a main character!\r\nSuccessfully imported actor Bendick Trorey as a main character!\r\nSuccessfully imported actor Jocelyne Dartnell as a main character!\r\nSuccessfully imported actor Kessia Oliphant as a main character!\r\nSuccessfully imported actor Roderick Kettlestringe as a lesser character!\r\nSuccessfully imported actor Waylan Durand as a lesser character!\r\nSuccessfully imported actor Vittorio Baise as a lesser character!\r\nSuccessfully imported actor Wanda Commings as a lesser character!\r\nSuccessfully imported actor Chris Lindley as a main character!\r\nSuccessfully imported actor Carce Nuth as a main character!\r\nSuccessfully imported actor Sela Hillett as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Jude Jeaneau as a lesser character!\r\nSuccessfully imported actor Edmon Busst as a lesser character!\r\nSuccessfully imported actor Aubrey Syms as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Kitti Leisman as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Filide Wicks as a lesser character!\r\nSuccessfully imported actor Ainslie Scollan as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Vincenty Dodsworth as a main character!\r\nSuccessfully imported actor Rochette Polleye as a main character!\r\nSuccessfully imported actor Zacherie Rainforth as a main character!\r\nSuccessfully imported actor Madella Van Son as a main character!\r\nSuccessfully imported actor Troy Lawlee as a lesser character!\r\nSuccessfully imported actor Cheri Lashley as a lesser character!\r\nSuccessfully imported actor Lissa Pummery as a lesser character!\r\nSuccessfully imported actor Adoree Boothby as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Maurizia Artus as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Carmon Pirdy as a lesser character!\r\nSuccessfully imported actor Charleen Blemings as a lesser character!\r\nSuccessfully imported actor Aubert Shama as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Waring Ducaen as a main character!\r\nSuccessfully imported actor Joelle Pinkstone as a lesser character!\r\nSuccessfully imported actor Yettie Copping as a main character!\r\nSuccessfully imported actor Wes Thoumasson as a lesser character!\r\nSuccessfully imported actor Clerissa Fellgate as a main character!\r\nSuccessfully imported actor Durand Rapper as a main character!\r\nSuccessfully imported actor Celene Whelpdale as a lesser character!\r\nSuccessfully imported actor Bryna Alti as a lesser character!\r\nSuccessfully imported actor Munroe Bringloe as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Agatha Tomashov as a main character!\r\nSuccessfully imported actor Ari Huriche as a lesser character!\r\nSuccessfully imported actor Carlyle Rack as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Abbe Bage as a main character!\r\nSuccessfully imported actor Aindrea Bellwood as a lesser character!\r\nSuccessfully imported actor Caye Blacklawe as a main character!\r\nSuccessfully imported actor Renault Kevlin as a lesser character!\r\nSuccessfully imported actor Marcello Brothers as a main character!\r\nSuccessfully imported actor Aurore Blazej as a main character!\r\nSuccessfully imported actor Torin Rylett as a main character!\r\nSuccessfully imported actor Sarita Velde as a lesser character!\r\nSuccessfully imported actor Donall Haggett as a lesser character!\r\nSuccessfully imported actor Annabel Elvidge as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Loutitia Joy as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Amy Haskell as a lesser character!\r\nSuccessfully imported actor Elyse Innocenti as a lesser character!\r\nSuccessfully imported actor Briney Hazel as a lesser character!\r\nSuccessfully imported actor Tyson Antonellini as a main character!\r\nSuccessfully imported actor Athene Blaylock as a lesser character!\r\nSuccessfully imported actor Ted Charley as a lesser character!\r\nSuccessfully imported actor Pammie Siege as a lesser character!\r\nSuccessfully imported actor Kellina Daingerfield as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Franny Hopewell as a lesser character!\r\nSuccessfully imported actor Gregoire Hardan as a main character!\r\nSuccessfully imported actor Janka Atkin as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Isadore Renol as a lesser character!\r\nSuccessfully imported actor Jemimah Biggadyke as a lesser character!\r\nSuccessfully imported actor Gay Sabie as a lesser character!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Rory Jarad as a main character!\r\nSuccessfully imported actor Hildagarde Itzkowicz as a main character!\r\nSuccessfully imported actor Corny Pegrum as a lesser character!\r\nSuccessfully imported actor Alica O'Hanley as a main character!\r\nSuccessfully imported actor Legra Strong as a main character!\r\nSuccessfully imported actor Arel Corps as a main character!\r\nSuccessfully imported actor Beauregard Hagyard as a lesser character!\r\nSuccessfully imported actor Nannie Embra as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Sylvia Felipe as a main character!\r\nSuccessfully imported actor Dorrie Winstanley as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Rycca Klimkov as a main character!\r\nSuccessfully imported actor Tobin Tamburi as a main character!\r\nSuccessfully imported actor Alexine Rickhuss as a main character!\r\nSuccessfully imported actor Si Grigoryev as a main character!\r\nSuccessfully imported actor Daloris Cornewell as a lesser character!\r\nSuccessfully imported actor Louie Sherratt as a main character!\r\nSuccessfully imported actor Marcile Donke as a lesser character!\r\nSuccessfully imported actor Christian Geere as a main character!\r\nSuccessfully imported actor Rafaelita Karp as a main character!\r\nSuccessfully imported actor Dedie Northcote as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Loria Gowdy as a main character!\r\nSuccessfully imported actor Virgilio Kunat as a lesser character!\r\nSuccessfully imported actor Alair Cattonnet as a main character!\r\nSuccessfully imported actor Linnea Edgeley as a main character!\r\nSuccessfully imported actor Shep Eck as a lesser character!\r\nSuccessfully imported actor Renell Rosenbush as a main character!\r\nSuccessfully imported actor Bobbe Service as a lesser character!\r\nSuccessfully imported actor Ellie Emtage as a lesser character!\r\nSuccessfully imported actor Evita Ridler as a main character!\r\nSuccessfully imported actor Smitty Beaty as a main character!\r\nSuccessfully imported actor Delphine Crone as a lesser character!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Cindelyn Mance as a main character!\r\nSuccessfully imported actor Maisie Hymas as a main character!\r\nSuccessfully imported actor Steven Lanfer as a main character!\r\nSuccessfully imported actor Joseito Melmoth as a lesser character!\r\nSuccessfully imported actor Robbert Tuvey as a main character!\r\nSuccessfully imported actor Rodney O'Neill as a main character!\r\nSuccessfully imported actor Cosetta Mauditt as a main character!\r\nSuccessfully imported actor Pippy Ennever as a lesser character!\r\nSuccessfully imported actor Clarie Ethelston as a lesser character!\r\nSuccessfully imported actor Shelby Luety as a main character!\r\nSuccessfully imported actor Edouard Idle as a main character!\r\nSuccessfully imported actor Abbi Sandwich as a main character!\r\nSuccessfully imported actor Ingram Raybould as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Maryjane Rikard as a lesser character!\r\nSuccessfully imported actor Miles Kantor as a lesser character!\r\nSuccessfully imported actor Trever Hulburd as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Isahella Rakestraw as a lesser character!\r\nSuccessfully imported actor Matthew McFetrich as a lesser character!\r\nSuccessfully imported actor Ennis Fisbey as a main character!\r\nSuccessfully imported actor Carina Donnison as a lesser character!\r\nSuccessfully imported actor Odilia Jasiak as a main character!\r\nSuccessfully imported actor Carley Lomansey as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Nance Pollie as a main character!\r\nSuccessfully imported actor Chilton D'Ambrogi as a main character!\r\nSuccessfully imported actor Marquita Brugger as a lesser character!\r\nSuccessfully imported actor Milo Kemp as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Isis Bessey as a main character!\r\nSuccessfully imported actor Colet Biernat as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Emily Crawforth as a main character!\r\nSuccessfully imported actor Tricia Lantry as a main character!\r\nSuccessfully imported actor Branden Drewe as a main character!\r\nSuccessfully imported actor Barron Atwill as a lesser character!\r\nSuccessfully imported actor Ardis Fellnee as a main character!\r\nSuccessfully imported actor Adolf Gally as a main character!\r\nSuccessfully imported actor Andriana Joskowicz as a main character!\r\nSuccessfully imported actor Averil Habbert as a lesser character!\r\nSuccessfully imported actor Welsh Bomb as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Kenny Shegog as a main character!\r\nSuccessfully imported actor Mabel Belfit as a main character!\r\nSuccessfully imported actor Salli Gierck as a main character!\r\nSuccessfully imported actor Nessie Dodgshun as a main character!\r\nSuccessfully imported actor Corina Wilce as a lesser character!\r\nSuccessfully imported actor Petronia Powter as a lesser character!\r\nSuccessfully imported actor Leupold Prenty as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Raymund Odlin as a main character!\r\nSuccessfully imported actor Cornie Schottli as a main character!\r\nSuccessfully imported actor Wilmar Ashwell as a lesser character!\r\nSuccessfully imported actor Gardener Josselsohn as a lesser character!\r\nSuccessfully imported actor Sybilla Aspin as a lesser character!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Lorraine Thorwarth as a main character!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Oralle Bladder as a main character!\r\nSuccessfully imported actor Wylma Tillerton as a main character!\r\nSuccessfully imported actor Theodosia Carlton as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Grover Petherick as a main character!\r\nSuccessfully imported actor Elsworth Francioli as a lesser character!\r\nSuccessfully imported actor Esteban Tander as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Ingaborg Oakenfield as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Webster Filson as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Mattie GiacobbiniJacob as a main character!\r\nSuccessfully imported actor Whitaker Ozelton as a lesser character!\r\nSuccessfully imported actor Melisandra Tranckle as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Shirlene Canedo as a main character!\r\nSuccessfully imported actor Decca Barles as a main character!\r\nSuccessfully imported actor Rhianna McLurg as a main character!\r\nSuccessfully imported actor Livia Skillen as a main character!\r\nSuccessfully imported actor Rubina Woolard as a lesser character!\r\nSuccessfully imported actor Iolanthe Wharrier as a lesser character!\r\nSuccessfully imported actor Rubi Baptist as a lesser character!\r\nSuccessfully imported actor Elwyn Callister as a main character!\r\nSuccessfully imported actor Jayme Bakes as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Hersh Birdis as a main character!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported actor Nathalie Neiland as a main character!\r\nSuccessfully imported actor Clarita Quarterman as a main character!\r\nSuccessfully imported actor Jaquith Kohter as a lesser character!\r\nSuccessfully imported actor Thadeus Filgate as a main character!\r\nSuccessfully imported actor Jeane Freen as a lesser character!\r\nSuccessfully imported actor Wenona Collumbell as a lesser character!\r\nSuccessfully imported actor Monro Cord as a main character!\r\nSuccessfully imported actor Sigrid Rhoddie as a lesser character!\r\nSuccessfully imported actor Engelbert Maharg as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Mariann Speerman as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Antony Kempe as a lesser character!\r\nSuccessfully imported actor Reamonn Maleby as a main character!\r\nSuccessfully imported actor Emelda Yule as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Aura Wauchope as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Codie Demaine as a lesser character!\r\nSuccessfully imported actor Sarine Tidgewell as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Keir Copnall as a lesser character!\r\nSuccessfully imported actor Giacomo Belward as a main character!\r\nInvalid data!\r\nSuccessfully imported actor Clement Pykett as a main character!\r\nSuccessfully imported actor Solomon Pinwill as a main character!\r\nSuccessfully imported actor Shermy Iskow as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Emmott Ditts as a lesser character!\r\nSuccessfully imported actor Hatty Friary as a main character!\r\nSuccessfully imported actor Zonnya Miner as a lesser character!\r\nInvalid data!\r\nSuccessfully imported actor Adelaida Hadlow as a lesser character!\r\nSuccessfully imported actor Tarrah Scouler as a lesser character!\r\nSuccessfully imported actor Morganica Irons as a lesser character!\r\nSuccessfully imported actor Whitney Standering as a main character!";

        var assertContext = this.serviceProvider.GetService<TheatreContext>();

        const int expectedProjectionCount = 287;
        var actualProjectionCount = assertContext.Casts.Count();

        Assert.That(actualProjectionCount, Is.EqualTo(expectedProjectionCount),
            $"Inserted {nameof(context.Casts)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Theatre.DataProcessor.Deserializer.ImportCasts)} output is incorrect!");
    }
     
    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection()
          .AddDbContext<TContext>(t => t
          .UseInMemoryDatabase(Guid.NewGuid().ToString())
          );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}