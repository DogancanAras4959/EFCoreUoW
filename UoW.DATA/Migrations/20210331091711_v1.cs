using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BASVURU_DURUM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurumAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Renk = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASVURU_DURUM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ETIKETLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtiketAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETIKETLER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KAMPANYA_KOD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KAMPANYA_KOD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KARGO_DETAY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HazirlanmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EklenmEtarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimEdildiMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KARGO_DETAY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KARGO_FIRMALARI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KargoFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KargoAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KargoUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KargoAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KARGO_FIRMALARI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ODEME_TURU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Odeme = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ODEME_TURU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SEHIR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEHIR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YETKI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YETKI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BASVURU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasvuruNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasvuruTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BayiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BayiUnvani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiliAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    basvuruFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasvuruDurumID = table.Column<int>(type: "int", nullable: false),
                    BasvuruDurum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASVURU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BASVURU_BASVURU_DURUM_BasvuruDurum",
                        column: x => x.BasvuruDurum,
                        principalTable: "BASVURU_DURUM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ILCE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SehirID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILCE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ILCE_SEHIR_SehirID",
                        column: x => x.SehirID,
                        principalTable: "SEHIR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KULLANICI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilResim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YetkiID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KULLANICI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KULLANICI_YETKI_YetkiID",
                        column: x => x.YetkiID,
                        principalTable: "YETKI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BASVURU_DOSYALAR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosyaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosyaTuru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosyaBoyutu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasvuruID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASVURU_DOSYALAR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BASVURU_DOSYALAR_BASVURU_BasvuruID",
                        column: x => x.BasvuruID,
                        principalTable: "BASVURU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BASVURU_AYAR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasvuruLimitGunluk = table.Column<int>(type: "int", nullable: false),
                    BasvuruAl = table.Column<bool>(type: "bit", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    SecilenAyar = table.Column<bool>(type: "bit", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASVURU_AYAR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BASVURU_AYAR_KULLANICI_YoneticiID",
                        column: x => x.YoneticiID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FATURA_TIP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FATURA_TIP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FATURA_TIP_KULLANICI_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KATEGORİLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    KategoriFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UstKategori = table.Column<int>(type: "int", nullable: true),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    UrunSayi = table.Column<int>(type: "int", nullable: false),
                    UrunEklendiMi = table.Column<bool>(type: "bit", nullable: false),
                    kullaniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KATEGORİLER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KATEGORİLER_KULLANICI_kullaniciID",
                        column: x => x.kullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MARKA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkaAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkaResim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    UstMarkaID = table.Column<int>(type: "int", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    kullaniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARKA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MARKA_KULLANICI_kullaniciID",
                        column: x => x.kullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MUSTERI_GRUBU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrubAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    IskontoOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    kullaniciGrupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERI_GRUBU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MUSTERI_GRUBU_KULLANICI_kullaniciGrupID",
                        column: x => x.kullaniciGrupID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SLIDER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsUse = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLIDER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SLIDER_KULLANICI_YoneticiID",
                        column: x => x.YoneticiID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOK_BIRIMI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    kullaniciBirimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOK_BIRIMI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STOK_BIRIMI_KULLANICI_kullaniciBirimID",
                        column: x => x.kullaniciBirimID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BAYI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BayiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UyeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BayiGrubuID = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeID = table.Column<int>(type: "int", nullable: false),
                    RiskLimiti = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AltBayiHakki = table.Column<bool>(type: "bit", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KargoYonet = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    KargoFirmasiID = table.Column<int>(type: "int", nullable: false),
                    musteriGrubuID = table.Column<int>(type: "int", nullable: true),
                    kullaniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAYI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BAYI_KARGO_FIRMALARI_KargoFirmasiID",
                        column: x => x.KargoFirmasiID,
                        principalTable: "KARGO_FIRMALARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAYI_KULLANICI_kullaniciID",
                        column: x => x.kullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BAYI_MUSTERI_GRUBU_musteriGrubuID",
                        column: x => x.musteriGrubuID,
                        principalTable: "MUSTERI_GRUBU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BAYI_ODEME_TURU_OdemeID",
                        column: x => x.OdemeID,
                        principalTable: "ODEME_TURU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MUSTERI_GRUP_KARGO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KargoID = table.Column<int>(type: "int", nullable: false),
                    MusteriGrupID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    Eklenme = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERI_GRUP_KARGO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MUSTERI_GRUP_KARGO_KARGO_FIRMALARI_KargoID",
                        column: x => x.KargoID,
                        principalTable: "KARGO_FIRMALARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MUSTERI_GRUP_KARGO_MUSTERI_GRUBU_MusteriGrupID",
                        column: x => x.MusteriGrupID,
                        principalTable: "MUSTERI_GRUBU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SLIDER_BANNER_ITEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderBy = table.Column<byte>(type: "tinyint", nullable: false),
                    SliderID = table.Column<int>(type: "int", nullable: false),
                    KampanyaKodu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLIDER_BANNER_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SLIDER_BANNER_ITEM_SLIDER_SliderID",
                        column: x => x.SliderID,
                        principalTable: "SLIDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SLIDER_ITEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTıtle = table.Column<bool>(type: "bit", nullable: false),
                    OrderBy = table.Column<byte>(type: "tinyint", nullable: false),
                    SliderID = table.Column<int>(type: "int", nullable: false),
                    KampanyaKodu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLIDER_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SLIDER_ITEM_SLIDER_SliderID",
                        column: x => x.SliderID,
                        principalTable: "SLIDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "URUNLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunSpot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunBarkodNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    MaxAdet = table.Column<int>(type: "int", nullable: false),
                    MinAdet = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FiyatBirimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KDV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KDVFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndirimOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OzelMi = table.Column<bool>(type: "bit", nullable: false),
                    IndirimliFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriID = table.Column<int>(type: "int", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    UstUrunID = table.Column<int>(type: "int", nullable: true),
                    WebServisKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkaID = table.Column<int>(type: "int", nullable: false),
                    StokBirimID = table.Column<int>(type: "int", nullable: false),
                    Etiketler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    BildirimVarmi = table.Column<bool>(type: "bit", nullable: false),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetayResim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    En = table.Column<double>(type: "float", nullable: false),
                    Boy = table.Column<double>(type: "float", nullable: false),
                    Derinlik = table.Column<double>(type: "float", nullable: false),
                    Agirlik = table.Column<double>(type: "float", nullable: false),
                    Desi = table.Column<double>(type: "float", nullable: false),
                    kullaniciID = table.Column<int>(type: "int", nullable: true),
                    stokbirimiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUNLER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUNLER_KATEGORİLER_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "KATEGORİLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_URUNLER_KULLANICI_kullaniciID",
                        column: x => x.kullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_URUNLER_MARKA_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "MARKA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_URUNLER_STOK_BIRIMI_stokbirimiID",
                        column: x => x.stokbirimiID,
                        principalTable: "STOK_BIRIMI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BAYI_ADRESLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeslimatAdresAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SehirID = table.Column<int>(type: "int", nullable: false),
                    IlceID = table.Column<int>(type: "int", nullable: false),
                    SiparisNotu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BayiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAYI_ADRESLER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BAYI_ADRESLER_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAYI_ADRESLER_ILCE_IlceID",
                        column: x => x.IlceID,
                        principalTable: "ILCE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAYI_ADRESLER_SEHIR_SehirID",
                        column: x => x.SehirID,
                        principalTable: "SEHIR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CUZDAN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bakiye = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    OlusturulmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuzdanNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuzdanTipi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUZDAN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUZDAN_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KARGO_SURE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    KargoID = table.Column<int>(type: "int", nullable: false),
                    Baslangic = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bitis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KARGO_SURE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KARGO_SURE_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KARGO_SURE_KARGO_FIRMALARI_KargoID",
                        column: x => x.KargoID,
                        principalTable: "KARGO_FIRMALARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UST_URUN_LISTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UST_URUN_LISTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UST_URUN_LISTE_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YETKILILER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiliAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YetkiliFoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YETKILILER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YETKILILER_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ETIKET_GONDERI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtiketID = table.Column<int>(type: "int", nullable: false),
                    GonderiID = table.Column<int>(type: "int", nullable: false),
                    etiketlerID = table.Column<int>(type: "int", nullable: true),
                    urunlerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETIKET_GONDERI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ETIKET_GONDERI_ETIKETLER_etiketlerID",
                        column: x => x.etiketlerID,
                        principalTable: "ETIKETLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ETIKET_GONDERI_URUNLER_urunlerID",
                        column: x => x.urunlerID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MUSTERI_GRUP_OZEL_URUN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriGrupID = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    Eklenme = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERI_GRUP_OZEL_URUN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MUSTERI_GRUP_OZEL_URUN_MUSTERI_GRUBU_MusteriGrupID",
                        column: x => x.MusteriGrupID,
                        principalTable: "MUSTERI_GRUBU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MUSTERI_GRUP_OZEL_URUN_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SLIDER_URUN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    SliderItemID = table.Column<int>(type: "int", nullable: false),
                    SliderBannerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLIDER_URUN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SLIDER_URUN_SLIDER_BANNER_ITEM_SliderBannerID",
                        column: x => x.SliderBannerID,
                        principalTable: "SLIDER_BANNER_ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SLIDER_URUN_SLIDER_ITEM_SliderItemID",
                        column: x => x.SliderItemID,
                        principalTable: "SLIDER_ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SLIDER_URUN_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOK_BILDIRIM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BayiId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    BildirimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOK_BILDIRIM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STOK_BILDIRIM_BAYI_BayiId",
                        column: x => x.BayiId,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STOK_BILDIRIM_URUNLER_UrunId",
                        column: x => x.UrunId,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "URUN_BEGENI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    aktifMi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Urun = table.Column<int>(type: "int", nullable: true),
                    Bayi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUN_BEGENI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUN_BEGENI_BAYI_Bayi",
                        column: x => x.Bayi,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_URUN_BEGENI_URUNLER_Urun",
                        column: x => x.Urun,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "URUN_FIYAT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdetBaslangic = table.Column<int>(type: "int", nullable: false),
                    AdetBitis = table.Column<int>(type: "int", nullable: false),
                    UrunFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUN_FIYAT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUN_FIYAT_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "URUN_RESİMLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResimYol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUN_RESİMLER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUN_RESİMLER_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "URUN_STOKLARI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUN_STOKLARI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUN_STOKLARI_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_URUN_STOKLARI_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SIPARIS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiparisTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HazirlanmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    BayiAdresID = table.Column<int>(type: "int", nullable: false),
                    TeslimatAdresID = table.Column<int>(type: "int", nullable: false),
                    ToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Indirim = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KargoBedeli = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    OdemeSekliID = table.Column<int>(type: "int", nullable: false),
                    TeslimatAdres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiparisNotu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIPARIS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SIPARIS_BAYI_ADRESLER_BayiAdresID",
                        column: x => x.BayiAdresID,
                        principalTable: "BAYI_ADRESLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SIPARIS_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SIPARIS_ODEME_TURU_OdemeSekliID",
                        column: x => x.OdemeSekliID,
                        principalTable: "ODEME_TURU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CUZDAN_ISLEMLER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IslemNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuzdanID = table.Column<int>(type: "int", nullable: false),
                    IslemDurum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdemeSekliID = table.Column<int>(type: "int", nullable: false),
                    IslemTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUZDAN_ISLEMLER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUZDAN_ISLEMLER_CUZDAN_CuzdanID",
                        column: x => x.CuzdanID,
                        principalTable: "CUZDAN",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CUZDAN_ISLEMLER_ODEME_TURU_OdemeSekliID",
                        column: x => x.OdemeSekliID,
                        principalTable: "ODEME_TURU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "URUN_LISTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    ListeUstID = table.Column<int>(type: "int", nullable: false),
                    aktifMi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Urun = table.Column<int>(type: "int", nullable: true),
                    Bayi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URUN_LISTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_URUN_LISTE_BAYI_Bayi",
                        column: x => x.Bayi,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_URUN_LISTE_URUNLER_Urun",
                        column: x => x.Urun,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_URUN_LISTE_UST_URUN_LISTE_ListeUstID",
                        column: x => x.ListeUstID,
                        principalTable: "UST_URUN_LISTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BAYI_TICARI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaUnvani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiDairesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSiteAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpostaAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SehirID = table.Column<int>(type: "int", nullable: false),
                    IlceID = table.Column<int>(type: "int", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    FirmaYetkiliID = table.Column<int>(type: "int", nullable: false),
                    PostaKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuhasebeMutabakatTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuhasebeMutabakatEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBanNoTL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBanNoUSD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBanNoEUR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAYI_TICARI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BAYI_TICARI_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAYI_TICARI_ILCE_IlceID",
                        column: x => x.IlceID,
                        principalTable: "ILCE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAYI_TICARI_SEHIR_SehirID",
                        column: x => x.SehirID,
                        principalTable: "SEHIR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BAYI_TICARI_YETKILILER_FirmaYetkiliID",
                        column: x => x.FirmaYetkiliID,
                        principalTable: "YETKILILER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BULTEN_UYELIKLERI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiliID = table.Column<int>(type: "int", nullable: false),
                    EmailAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdinizSoyadiniz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BULTEN_UYELIKLERI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BULTEN_UYELIKLERI_YETKILILER_YetkiliID",
                        column: x => x.YetkiliID,
                        principalTable: "YETKILILER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KARGO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    BayiAdresID = table.Column<int>(type: "int", nullable: false),
                    TeslimatAdresID = table.Column<int>(type: "int", nullable: false),
                    TeslimatAdresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SatinalmaNotu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KargoDetayID = table.Column<int>(type: "int", nullable: false),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    KargoFirmaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KARGO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KARGO_BAYI_ADRESLER_BayiAdresID",
                        column: x => x.BayiAdresID,
                        principalTable: "BAYI_ADRESLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KARGO_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KARGO_KARGO_DETAY_KargoDetayID",
                        column: x => x.KargoDetayID,
                        principalTable: "KARGO_DETAY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KARGO_KARGO_FIRMALARI_KargoFirmaID",
                        column: x => x.KargoFirmaID,
                        principalTable: "KARGO_FIRMALARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KARGO_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ODEME_DURUM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    OdemeZaman = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeTipiID = table.Column<int>(type: "int", nullable: false),
                    OnayKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdemeTamamMi = table.Column<bool>(type: "bit", nullable: false),
                    ToplamOdeme = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ODEME_DURUM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ODEME_DURUM_ODEME_TURU_OdemeTipiID",
                        column: x => x.OdemeTipiID,
                        principalTable: "ODEME_TURU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ODEME_DURUM_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OZEL_TEKLIF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    TeklifTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OZEL_TEKLIF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OZEL_TEKLIF_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OZEL_TEKLIF_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SEPET",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToplamSatır = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEPET", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SEPET_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SEPET_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FATURA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FaturaTipiID = table.Column<int>(type: "int", nullable: false),
                    BayiID = table.Column<int>(type: "int", nullable: false),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    BayiAdresID = table.Column<int>(type: "int", nullable: false),
                    ToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BayiTicariID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FATURA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FATURA_BAYI_ADRESLER_BayiAdresID",
                        column: x => x.BayiAdresID,
                        principalTable: "BAYI_ADRESLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FATURA_BAYI_BayiID",
                        column: x => x.BayiID,
                        principalTable: "BAYI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FATURA_BAYI_TICARI_BayiTicariID",
                        column: x => x.BayiTicariID,
                        principalTable: "BAYI_TICARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FATURA_FATURA_TIP_FaturaTipiID",
                        column: x => x.FaturaTipiID,
                        principalTable: "FATURA_TIP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FATURA_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BASVURU_BasvuruDurum",
                table: "BASVURU",
                column: "BasvuruDurum");

            migrationBuilder.CreateIndex(
                name: "IX_BASVURU_AYAR_YoneticiID",
                table: "BASVURU_AYAR",
                column: "YoneticiID");

            migrationBuilder.CreateIndex(
                name: "IX_BASVURU_DOSYALAR_BasvuruID",
                table: "BASVURU_DOSYALAR",
                column: "BasvuruID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_KargoFirmasiID",
                table: "BAYI",
                column: "KargoFirmasiID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_kullaniciID",
                table: "BAYI",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_musteriGrubuID",
                table: "BAYI",
                column: "musteriGrubuID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_OdemeID",
                table: "BAYI",
                column: "OdemeID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_ADRESLER_BayiID",
                table: "BAYI_ADRESLER",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_ADRESLER_IlceID",
                table: "BAYI_ADRESLER",
                column: "IlceID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_ADRESLER_SehirID",
                table: "BAYI_ADRESLER",
                column: "SehirID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_TICARI_BayiID",
                table: "BAYI_TICARI",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_TICARI_FirmaYetkiliID",
                table: "BAYI_TICARI",
                column: "FirmaYetkiliID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_TICARI_IlceID",
                table: "BAYI_TICARI",
                column: "IlceID");

            migrationBuilder.CreateIndex(
                name: "IX_BAYI_TICARI_SehirID",
                table: "BAYI_TICARI",
                column: "SehirID");

            migrationBuilder.CreateIndex(
                name: "IX_BULTEN_UYELIKLERI_YetkiliID",
                table: "BULTEN_UYELIKLERI",
                column: "YetkiliID");

            migrationBuilder.CreateIndex(
                name: "IX_CUZDAN_BayiID",
                table: "CUZDAN",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_CUZDAN_ISLEMLER_CuzdanID",
                table: "CUZDAN_ISLEMLER",
                column: "CuzdanID");

            migrationBuilder.CreateIndex(
                name: "IX_CUZDAN_ISLEMLER_OdemeSekliID",
                table: "CUZDAN_ISLEMLER",
                column: "OdemeSekliID");

            migrationBuilder.CreateIndex(
                name: "IX_ETIKET_GONDERI_etiketlerID",
                table: "ETIKET_GONDERI",
                column: "etiketlerID");

            migrationBuilder.CreateIndex(
                name: "IX_ETIKET_GONDERI_urunlerID",
                table: "ETIKET_GONDERI",
                column: "urunlerID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_BayiAdresID",
                table: "FATURA",
                column: "BayiAdresID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_BayiID",
                table: "FATURA",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_BayiTicariID",
                table: "FATURA",
                column: "BayiTicariID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_FaturaTipiID",
                table: "FATURA",
                column: "FaturaTipiID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_SiparisID",
                table: "FATURA",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_FATURA_TIP_KullaniciID",
                table: "FATURA_TIP",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_ILCE_SehirID",
                table: "ILCE",
                column: "SehirID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_BayiAdresID",
                table: "KARGO",
                column: "BayiAdresID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_BayiID",
                table: "KARGO",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_KargoDetayID",
                table: "KARGO",
                column: "KargoDetayID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_KargoFirmaID",
                table: "KARGO",
                column: "KargoFirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_SiparisID",
                table: "KARGO",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_SURE_BayiID",
                table: "KARGO_SURE",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_KARGO_SURE_KargoID",
                table: "KARGO_SURE",
                column: "KargoID");

            migrationBuilder.CreateIndex(
                name: "IX_KATEGORİLER_kullaniciID",
                table: "KATEGORİLER",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_KULLANICI_YetkiID",
                table: "KULLANICI",
                column: "YetkiID");

            migrationBuilder.CreateIndex(
                name: "IX_MARKA_kullaniciID",
                table: "MARKA",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_GRUBU_kullaniciGrupID",
                table: "MUSTERI_GRUBU",
                column: "kullaniciGrupID");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_GRUP_KARGO_KargoID",
                table: "MUSTERI_GRUP_KARGO",
                column: "KargoID");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_GRUP_KARGO_MusteriGrupID",
                table: "MUSTERI_GRUP_KARGO",
                column: "MusteriGrupID");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_GRUP_OZEL_URUN_MusteriGrupID",
                table: "MUSTERI_GRUP_OZEL_URUN",
                column: "MusteriGrupID");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_GRUP_OZEL_URUN_UrunID",
                table: "MUSTERI_GRUP_OZEL_URUN",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_ODEME_DURUM_OdemeTipiID",
                table: "ODEME_DURUM",
                column: "OdemeTipiID");

            migrationBuilder.CreateIndex(
                name: "IX_ODEME_DURUM_SiparisID",
                table: "ODEME_DURUM",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_OZEL_TEKLIF_BayiID",
                table: "OZEL_TEKLIF",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_OZEL_TEKLIF_SiparisID",
                table: "OZEL_TEKLIF",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_SEPET_SiparisID",
                table: "SEPET",
                column: "SiparisID");

            migrationBuilder.CreateIndex(
                name: "IX_SEPET_UrunID",
                table: "SEPET",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_BayiAdresID",
                table: "SIPARIS",
                column: "BayiAdresID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_BayiID",
                table: "SIPARIS",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_OdemeSekliID",
                table: "SIPARIS",
                column: "OdemeSekliID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_YoneticiID",
                table: "SLIDER",
                column: "YoneticiID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_BANNER_ITEM_SliderID",
                table: "SLIDER_BANNER_ITEM",
                column: "SliderID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_ITEM_SliderID",
                table: "SLIDER_ITEM",
                column: "SliderID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_URUN_SliderBannerID",
                table: "SLIDER_URUN",
                column: "SliderBannerID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_URUN_SliderItemID",
                table: "SLIDER_URUN",
                column: "SliderItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SLIDER_URUN_UrunID",
                table: "SLIDER_URUN",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_STOK_BILDIRIM_BayiId",
                table: "STOK_BILDIRIM",
                column: "BayiId");

            migrationBuilder.CreateIndex(
                name: "IX_STOK_BILDIRIM_UrunId",
                table: "STOK_BILDIRIM",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_STOK_BIRIMI_kullaniciBirimID",
                table: "STOK_BIRIMI",
                column: "kullaniciBirimID");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_BEGENI_Bayi",
                table: "URUN_BEGENI",
                column: "Bayi");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_BEGENI_Urun",
                table: "URUN_BEGENI",
                column: "Urun");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_FIYAT_UrunID",
                table: "URUN_FIYAT",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_LISTE_Bayi",
                table: "URUN_LISTE",
                column: "Bayi");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_LISTE_ListeUstID",
                table: "URUN_LISTE",
                column: "ListeUstID");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_LISTE_Urun",
                table: "URUN_LISTE",
                column: "Urun");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_RESİMLER_UrunID",
                table: "URUN_RESİMLER",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_STOKLARI_BayiID",
                table: "URUN_STOKLARI",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_URUN_STOKLARI_UrunID",
                table: "URUN_STOKLARI",
                column: "UrunID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_KategoriID",
                table: "URUNLER",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_kullaniciID",
                table: "URUNLER",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_MarkaID",
                table: "URUNLER",
                column: "MarkaID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_stokbirimiID",
                table: "URUNLER",
                column: "stokbirimiID");

            migrationBuilder.CreateIndex(
                name: "IX_UST_URUN_LISTE_BayiID",
                table: "UST_URUN_LISTE",
                column: "BayiID");

            migrationBuilder.CreateIndex(
                name: "IX_YETKILILER_BayiID",
                table: "YETKILILER",
                column: "BayiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BASVURU_AYAR");

            migrationBuilder.DropTable(
                name: "BASVURU_DOSYALAR");

            migrationBuilder.DropTable(
                name: "BULTEN_UYELIKLERI");

            migrationBuilder.DropTable(
                name: "CUZDAN_ISLEMLER");

            migrationBuilder.DropTable(
                name: "ETIKET_GONDERI");

            migrationBuilder.DropTable(
                name: "FATURA");

            migrationBuilder.DropTable(
                name: "KAMPANYA_KOD");

            migrationBuilder.DropTable(
                name: "KARGO");

            migrationBuilder.DropTable(
                name: "KARGO_SURE");

            migrationBuilder.DropTable(
                name: "MUSTERI_GRUP_KARGO");

            migrationBuilder.DropTable(
                name: "MUSTERI_GRUP_OZEL_URUN");

            migrationBuilder.DropTable(
                name: "ODEME_DURUM");

            migrationBuilder.DropTable(
                name: "OZEL_TEKLIF");

            migrationBuilder.DropTable(
                name: "SEPET");

            migrationBuilder.DropTable(
                name: "SLIDER_URUN");

            migrationBuilder.DropTable(
                name: "STOK_BILDIRIM");

            migrationBuilder.DropTable(
                name: "URUN_BEGENI");

            migrationBuilder.DropTable(
                name: "URUN_FIYAT");

            migrationBuilder.DropTable(
                name: "URUN_LISTE");

            migrationBuilder.DropTable(
                name: "URUN_RESİMLER");

            migrationBuilder.DropTable(
                name: "URUN_STOKLARI");

            migrationBuilder.DropTable(
                name: "BASVURU");

            migrationBuilder.DropTable(
                name: "CUZDAN");

            migrationBuilder.DropTable(
                name: "ETIKETLER");

            migrationBuilder.DropTable(
                name: "BAYI_TICARI");

            migrationBuilder.DropTable(
                name: "FATURA_TIP");

            migrationBuilder.DropTable(
                name: "KARGO_DETAY");

            migrationBuilder.DropTable(
                name: "SIPARIS");

            migrationBuilder.DropTable(
                name: "SLIDER_BANNER_ITEM");

            migrationBuilder.DropTable(
                name: "SLIDER_ITEM");

            migrationBuilder.DropTable(
                name: "UST_URUN_LISTE");

            migrationBuilder.DropTable(
                name: "URUNLER");

            migrationBuilder.DropTable(
                name: "BASVURU_DURUM");

            migrationBuilder.DropTable(
                name: "YETKILILER");

            migrationBuilder.DropTable(
                name: "BAYI_ADRESLER");

            migrationBuilder.DropTable(
                name: "SLIDER");

            migrationBuilder.DropTable(
                name: "KATEGORİLER");

            migrationBuilder.DropTable(
                name: "MARKA");

            migrationBuilder.DropTable(
                name: "STOK_BIRIMI");

            migrationBuilder.DropTable(
                name: "BAYI");

            migrationBuilder.DropTable(
                name: "ILCE");

            migrationBuilder.DropTable(
                name: "KARGO_FIRMALARI");

            migrationBuilder.DropTable(
                name: "MUSTERI_GRUBU");

            migrationBuilder.DropTable(
                name: "ODEME_TURU");

            migrationBuilder.DropTable(
                name: "SEHIR");

            migrationBuilder.DropTable(
                name: "KULLANICI");

            migrationBuilder.DropTable(
                name: "YETKI");
        }
    }
}
