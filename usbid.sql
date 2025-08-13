/*
 Navicat Premium Dump SQL

 Source Server         : Pg_16.9
 Source Server Type    : PostgreSQL
 Source Server Version : 160009 (160009)
 Source Host           : localhost:5433
 Source Catalog        : postgres
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 160009 (160009)
 File Encoding         : 65001

 Date: 12/08/2025 17:52:17
*/


-- ----------------------------
-- Table structure for usbid
-- ----------------------------
DROP TABLE IF EXISTS "public"."usbid";
CREATE TABLE "public"."usbid" (
  "eusb_id" varchar(16) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "username" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "mail" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "company" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "mobiletel" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "actflag" numeric(1,0) NOT NULL DEFAULT 1,
  "keypublisherdate" timestamp(6) NOT NULL DEFAULT now(),
  "keyupdatedate" timestamp(6) NOT NULL DEFAULT now(),
  "usestartday" timestamp(6) DEFAULT now(),
  "useendday" timestamp(6) DEFAULT now(),
  "functions" varchar(8) COLLATE "pg_catalog"."default" NOT NULL DEFAULT '00000000'::bpchar,
  "useefustartday" timestamp(6) NOT NULL DEFAULT now(),
  "useefuendday" timestamp(6) NOT NULL DEFAULT now(),
  "dincad" varchar(1) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "cadversion" numeric(2,0) NOT NULL DEFAULT 0,
  "cadtarget" numeric(1,0) NOT NULL DEFAULT 0,
  "cadopuseendday" timestamp(6) NOT NULL DEFAULT now(),
  "cadopfunctions" varchar(10) COLLATE "pg_catalog"."default" NOT NULL DEFAULT '0000000000'::bpchar,
  "tmp" numeric(1,0) NOT NULL DEFAULT 0,
  "dinsubcon" numeric(1,0) NOT NULL DEFAULT 0,
  "notes" varchar(1200) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ' '::bpchar,
  "updateflg" numeric(1,0) NOT NULL DEFAULT 0
)
;
COMMENT ON COLUMN "public"."usbid"."eusb_id" IS 'キーＩＤ
キー名：usbid_pkey';
COMMENT ON COLUMN "public"."usbid"."username" IS '利用者';
COMMENT ON COLUMN "public"."usbid"."mail" IS 'メール';
COMMENT ON COLUMN "public"."usbid"."company" IS '会社名';
COMMENT ON COLUMN "public"."usbid"."mobiletel" IS '携帯電話';
COMMENT ON COLUMN "public"."usbid"."actflag" IS '活動フラグ
1：.活動中 2：故障 3：紛失 4：失効など';
COMMENT ON COLUMN "public"."usbid"."keypublisherdate" IS 'Key作成日';
COMMENT ON COLUMN "public"."usbid"."keyupdatedate" IS 'Key更新日';
COMMENT ON COLUMN "public"."usbid"."usestartday" IS '加工帳入力利用開始日
加工帳入力システム用';
COMMENT ON COLUMN "public"."usbid"."useendday" IS '加工帳入力利用終了日
加工帳入力システム用';
COMMENT ON COLUMN "public"."usbid"."functions" IS '加工帳入力システム機能
XXXXXXXX　1：利用可
1：CAD　
2：重量表　
3：QR　
4：特殊計算　
5：ハンチ入力　
6：Datx復元
7：SUMファイル
8：加工帳絵符OP';
COMMENT ON COLUMN "public"."usbid"."useefustartday" IS '加工帳絵符利用開始日
加工帳絵符OPは有効の時、設定する';
COMMENT ON COLUMN "public"."usbid"."useefuendday" IS '加工帳絵符利用終了日
加工帳絵符OPは有効の時、設定する';
COMMENT ON COLUMN "public"."usbid"."dincad" IS 'CADグレードコントロール(Level)
2：DINCAD30
4：DINCAD50
6：DINCAD100';
COMMENT ON COLUMN "public"."usbid"."cadversion" IS 'CADバージョンコントロール
YY(西暦下二桁)（対象バージョン）';
COMMENT ON COLUMN "public"."usbid"."cadtarget" IS 'DINCAD2ターゲットCAD
対象CAD';
COMMENT ON COLUMN "public"."usbid"."cadopuseendday" IS 'CAD・加工帳オプション機能使用終了日';
COMMENT ON COLUMN "public"."usbid"."cadopfunctions" IS 'CAD・加工帳オプション機能
XXXXXXXX　1：利用可';
COMMENT ON COLUMN "public"."usbid"."tmp" IS 'TPMシステム利用状態
1：TPM使う　0：TPM使わない';
COMMENT ON COLUMN "public"."usbid"."dinsubcon" IS 'アイコーサブコンフラグ
1：使用可 0：使用不可';
COMMENT ON COLUMN "public"."usbid"."notes" IS '備考';
COMMENT ON COLUMN "public"."usbid"."updateflg" IS '自働更新
1：自動更新可 0：自動更新不可';

-- ----------------------------
-- Primary Key structure for table usbid
-- ----------------------------
ALTER TABLE "public"."usbid" ADD CONSTRAINT "Access_pkey" PRIMARY KEY ("eusb_id");
