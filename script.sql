USE [master]
GO
/****** Object:  Database [RecipeGiver]    Script Date: 11/26/2024 11:02:16 PM ******/
CREATE DATABASE [RecipeGiver]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RecipeGiver', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RecipeGiver.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RecipeGiver_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RecipeGiver_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RecipeGiver] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RecipeGiver].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RecipeGiver] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RecipeGiver] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RecipeGiver] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RecipeGiver] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RecipeGiver] SET ARITHABORT OFF 
GO
ALTER DATABASE [RecipeGiver] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RecipeGiver] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RecipeGiver] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RecipeGiver] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RecipeGiver] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RecipeGiver] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RecipeGiver] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RecipeGiver] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RecipeGiver] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RecipeGiver] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RecipeGiver] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RecipeGiver] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RecipeGiver] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RecipeGiver] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RecipeGiver] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RecipeGiver] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RecipeGiver] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RecipeGiver] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RecipeGiver] SET  MULTI_USER 
GO
ALTER DATABASE [RecipeGiver] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RecipeGiver] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RecipeGiver] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RecipeGiver] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RecipeGiver] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RecipeGiver] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RecipeGiver] SET QUERY_STORE = ON
GO
ALTER DATABASE [RecipeGiver] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RecipeGiver]
GO
/****** Object:  Table [dbo].[category]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [text] NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingredient]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredient](
	[ingredientId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[type] [varchar](255) NULL,
	[nutritionInfoId] [int] NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingredientNutritionInfo]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredientNutritionInfo](
	[ingredientId] [int] NOT NULL,
	[nutritionInfoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ingredientId] ASC,
	[nutritionInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nutritionInfo]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nutritionInfo](
	[nutritionInfoId] [int] IDENTITY(1,1) NOT NULL,
	[calories] [float] NULL,
	[protein] [float] NULL,
	[fat] [float] NULL,
	[carbs] [float] NULL,
	[fiber] [float] NULL,
	[sugar] [float] NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[nutritionInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[origin]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[origin](
	[originId] [int] IDENTITY(1,1) NOT NULL,
	[country] [varchar](255) NULL,
	[region] [varchar](255) NULL,
	[cultureDescription] [text] NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[originId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[paymentId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[userSubscriptionId] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[paymentDate] [datetime] NULL,
	[paymentMethod] [varchar](50) NULL,
	[status] [varchar](50) NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[recipeId] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NOT NULL,
	[description] [text] NULL,
	[preparationTime] [int] NULL,
	[cookingTime] [int] NULL,
	[servings] [int] NULL,
	[difficultyLevel] [varchar](50) NULL,
	[cookingMethod] [varchar](255) NULL,
	[categoryId] [int] NULL,
	[userId] [int] NULL,
	[originId] [int] NULL,
	[createdDate] [date] NULL,
	[lastUpdated] [date] NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipeIngredient]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipeIngredient](
	[recipeId] [int] NOT NULL,
	[ingredientId] [int] NOT NULL,
	[quantity] [float] NULL,
	[unit] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC,
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[step]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[step](
	[stepId] [int] IDENTITY(1,1) NOT NULL,
	[recipeId] [int] NOT NULL,
	[stepNumber] [int] NOT NULL,
	[description] [text] NULL,
	[image] [varchar](255) NULL,
	[duration] [int] NULL,
	[toolsRequired] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[stepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscription]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscription](
	[subscriptionId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [text] NULL,
	[price] [float] NOT NULL,
	[duration] [int] NOT NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[subscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[role] [varchar](50) NOT NULL,
	[fullname] [varchar](255) NULL,
	[email] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[viewedRecipe] [int] NULL,
	[activeStatus] [bit] NULL,
	[created_at] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userSubscription]    Script Date: 11/26/2024 11:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userSubscription](
	[userSubscriptionId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[subscriptionId] [int] NOT NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NOT NULL,
	[activeStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[userSubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (1, N'Desserts', N'Sweet treats and desserts', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (2, N'Main Course', N'Main course dishes', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (3, N'Appetizers', N'Starters and appetizers', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (4, N'Drinks', N'Beverages and cocktails', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (5, N'Salads', N'Healthy salads and sides', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (6, N'Snacks', N'Quick bites and snacks', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (7, N'Soups', N'Comforting soups and stews', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (8, N'Breakfast', N'Morning meals and ideas', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (9, N'Vegetarian', N'Meat-free recipes', 1)
INSERT [dbo].[category] ([categoryId], [name], [description], [activeStatus]) VALUES (10, N'Seafood', N'Recipes featuring fish and shellfish', 1)
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[nutritionInfo] ON 

INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (1, 250, 10, 8, 30, 3, 15, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (2, 450, 25, 12, 50, 5, 20, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (3, 150, 5, 2, 20, 2, 10, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (4, 300, 15, 10, 35, 4, 18, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (5, 500, 30, 15, 60, 6, 25, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (6, 200, 8, 5, 25, 3, 12, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (7, 350, 18, 8, 40, 5, 20, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (8, 400, 22, 10, 45, 5, 22, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (9, 600, 40, 20, 70, 8, 30, 1)
INSERT [dbo].[nutritionInfo] ([nutritionInfoId], [calories], [protein], [fat], [carbs], [fiber], [sugar], [activeStatus]) VALUES (10, 100, 2, 1, 10, 1, 5, 1)
SET IDENTITY_INSERT [dbo].[nutritionInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[origin] ON 

INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (1, N'Italy', N'Southern', N'Known for pasta and pizza', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (2, N'Japan', N'Eastern', N'Rich in sushi and seafood cuisine', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (3, N'France', N'Western', N'Famous for fine dining and pastries', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (4, N'India', N'Southern Asia', N'Rich in spices and curries', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (5, N'Mexico', N'Central America', N'Known for tacos and chili', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (6, N'Thailand', N'Southeast Asia', N'Famous for spicy and flavorful dishes', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (7, N'USA', N'Western', N'Burgers, BBQ, and fast food culture', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (8, N'China', N'Eastern Asia', N'Known for noodles and dumplings', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (9, N'Spain', N'Western Europe', N'Famous for tapas and paella', 1)
INSERT [dbo].[origin] ([originId], [country], [region], [cultureDescription], [activeStatus]) VALUES (10, N'Greece', N'Southern Europe', N'Known for Mediterranean flavors', 1)
SET IDENTITY_INSERT [dbo].[origin] OFF
GO
SET IDENTITY_INSERT [dbo].[recipe] ON 

INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (1, N'Spaghetti Carbonara', N'Classic Italian pasta dish with eggs, cheese, pancetta, and pepper.', 10, 20, 2, N'Easy', N'Boiling', 1, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (2, N'Tuna Sushi Roll', N'Delicious sushi rolls with fresh tuna.', 15, 0, 4, N'Medium', N'Raw', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (3, N'Chicken Curry', N'Flavorful chicken curry with spices and coconut milk.', 20, 30, 4, N'Medium', N'Stewing', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (4, N'French Crepes', N'Thin pancakes served with sweet or savory fillings.', 10, 10, 2, N'Easy', N'Frying', 1, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (5, N'Vegetable Stir Fry', N'Quick and healthy vegetable stir fry with soy sauce.', 10, 15, 3, N'Easy', N'Stir Frying', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (6, N'Chocolate Cake', N'Rich chocolate cake with a creamy frosting.', 30, 40, 8, N'Medium', N'Baking', 1, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (7, N'Beef Stew', N'Hearty beef stew with potatoes and carrots.', 20, 120, 5, N'Hard', N'Stewing', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (8, N'Salmon Teriyaki', N'Grilled salmon with a teriyaki glaze.', 15, 20, 2, N'Easy', N'Grilling', 2, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (9, N'Classic Cheeseburger', N'Juicy beef patty with cheese, lettuce, and tomato in a bun.', 10, 10, 1, N'Easy', N'Grilling', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (10, N'Caesar Salad', N'Crisp romaine lettuce with Caesar dressing and croutons.', 10, 0, 2, N'Easy', N'Raw', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (11, N'Margherita Pizza', N'Classic pizza with tomato sauce, mozzarella, and basil.', 20, 15, 4, N'Medium', N'Baking', 1, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (12, N'Beef Pho', N'Vietnamese noodle soup with beef and fragrant herbs.', 30, 60, 4, N'Medium', N'Boiling', 2, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (13, N'Pumpkin Soup', N'Creamy pumpkin soup with a hint of nutmeg.', 15, 30, 4, N'Easy', N'Blending', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (14, N'Egg Fried Rice', N'Simple and tasty fried rice with scrambled eggs.', 10, 10, 2, N'Easy', N'Stir Frying', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (15, N'Lasagna', N'Layers of pasta, meat sauce, and cheese baked to perfection.', 30, 45, 6, N'Medium', N'Baking', 1, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (16, N'Chicken Alfredo Pasta', N'Creamy pasta with chicken and Alfredo sauce.', 20, 20, 3, N'Medium', N'Boiling', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (17, N'Miso Soup', N'Japanese soup with miso paste, tofu, and seaweed.', 5, 10, 2, N'Easy', N'Boiling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (18, N'Apple Pie', N'Classic apple pie with a flaky crust.', 30, 60, 8, N'Medium', N'Baking', 1, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (19, N'Grilled Cheese Sandwich', N'Toasted bread with melted cheese inside.', 5, 5, 1, N'Easy', N'Frying', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (20, N'Spring Rolls', N'Fresh vegetable and shrimp rolls with a dipping sauce.', 20, 5, 4, N'Easy', N'Raw', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (21, N'Pancakes', N'Fluffy pancakes served with syrup or fruits.', 10, 10, 2, N'Easy', N'Frying', 1, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (22, N'Chicken Tacos', N'Soft tacos filled with spicy chicken and fresh salsa.', 20, 10, 3, N'Easy', N'Grilling', 2, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (23, N'Pad Thai', N'Thai stir-fried noodles with shrimp, peanuts, and tamarind sauce.', 15, 20, 4, N'Medium', N'Stir Frying', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (24, N'Lentil Soup', N'Hearty soup made with lentils, vegetables, and spices.', 15, 30, 4, N'Easy', N'Boiling', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (25, N'Fish and Chips', N'Crispy fried fish with golden fries.', 15, 20, 2, N'Easy', N'Frying', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (26, N'Vegetable Curry', N'Flavorful curry made with mixed vegetables and spices.', 20, 30, 4, N'Medium', N'Stewing', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (27, N'Grilled Lamb Chops', N'Juicy lamb chops marinated and grilled to perfection.', 10, 15, 2, N'Medium', N'Grilling', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (28, N'Tom Yum Soup', N'Spicy Thai soup with shrimp and fragrant herbs.', 15, 20, 4, N'Medium', N'Boiling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (29, N'BBQ Ribs', N'Tender pork ribs with a smoky BBQ sauce.', 20, 180, 4, N'Hard', N'Smoking', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (30, N'Mango Smoothie', N'Refreshing mango smoothie with yogurt.', 5, 0, 1, N'Easy', N'Blending', 4, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (31, N'Crispy Roast Duck', N'Golden-brown roast duck with a crispy skin.', 30, 120, 4, N'Hard', N'Roasting', 2, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (32, N'Stuffed Bell Peppers', N'Bell peppers filled with rice, meat, and vegetables.', 20, 40, 3, N'Medium', N'Baking', 3, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (33, N'Greek Salad', N'Refreshing salad with tomatoes, cucumbers, olives, and feta.', 10, 0, 2, N'Easy', N'Raw', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (34, N'Shrimp Scampi', N'Garlic butter shrimp served with pasta.', 10, 15, 2, N'Medium', N'Boiling', 2, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (35, N'Pork Dumplings', N'Steamed or fried dumplings filled with pork and vegetables.', 25, 10, 4, N'Medium', N'Steaming', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (36, N'Banana Bread', N'Moist and sweet banana bread.', 15, 50, 8, N'Medium', N'Baking', 1, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (37, N'Spinach Quiche', N'Savory quiche with spinach and cheese.', 20, 40, 6, N'Medium', N'Baking', 2, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (38, N'Ratatouille', N'French vegetable dish with zucchini, eggplant, and peppers.', 20, 30, 4, N'Medium', N'Baking', 3, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (39, N'Shepherd Pie', N'Comforting pie with minced meat and mashed potatoes.', 30, 40, 6, N'Medium', N'Baking', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (40, N'Chicken Biryani', N'Flavorful Indian rice dish with chicken and spices.', 30, 60, 4, N'Medium', N'Stewing', 2, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (41, N'Vegetarian Chili', N'Hearty chili with beans, tomatoes, and vegetables.', 20, 30, 4, N'Easy', N'Stewing', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (42, N'Blueberry Muffins', N'Sweet muffins bursting with fresh blueberries.', 15, 25, 12, N'Easy', N'Baking', 1, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (43, N'Fettuccine Alfredo', N'Creamy pasta dish with Parmesan cheese.', 10, 15, 2, N'Easy', N'Boiling', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (44, N'Shakshuka', N'Poached eggs in a spicy tomato sauce.', 10, 20, 2, N'Easy', N'Simmering', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (45, N'Pulled Pork Sandwich', N'Tender pulled pork served in a bun.', 20, 240, 6, N'Hard', N'Slow Cooking', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (46, N'Vegetable Sushi', N'Sushi rolls filled with fresh vegetables.', 20, 0, 4, N'Medium', N'Raw', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (47, N'Tomato Bruschetta', N'Toasted bread topped with tomatoes and basil.', 10, 5, 4, N'Easy', N'Toasting', 3, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (48, N'Beef Wellington', N'Tender beef fillet wrapped in pastry.', 40, 45, 4, N'Hard', N'Baking', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (49, N'Pumpkin Pie', N'Traditional pumpkin pie with warm spices.', 20, 50, 8, N'Medium', N'Baking', 1, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (50, N'Teriyaki Chicken', N'Chicken glazed with teriyaki sauce.', 10, 15, 4, N'Easy', N'Grilling', 2, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (51, N'Cauliflower Steak', N'Roasted cauliflower slices with spices.', 10, 30, 2, N'Easy', N'Roasting', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (52, N'Stuffed Mushrooms', N'Mushroom caps filled with breadcrumbs and cheese.', 10, 20, 4, N'Easy', N'Baking', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (53, N'Falafel Wrap', N'Crispy falafel balls wrapped in flatbread with veggies and sauce.', 15, 10, 2, N'Easy', N'Frying', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (54, N'Coconut Shrimp', N'Crispy fried shrimp coated in coconut flakes.', 15, 10, 2, N'Medium', N'Frying', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (55, N'Beetroot Salad', N'Fresh salad with beetroot, feta, and walnuts.', 10, 0, 2, N'Easy', N'Raw', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (56, N'Chicken Parmesan', N'Breaded chicken topped with marinara and cheese.', 20, 30, 2, N'Medium', N'Baking', 2, 2, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (57, N'Fish Tacos', N'Grilled fish with slaw and creamy sauce in soft tacos.', 15, 10, 2, N'Easy', N'Grilling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (58, N'Clam Chowder', N'Creamy soup with clams and potatoes.', 20, 30, 4, N'Medium', N'Simmering', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (59, N'Avocado Toast', N'Toasted bread topped with smashed avocado and seasonings.', 5, 5, 1, N'Easy', N'Raw', 3, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (60, N'Pesto Pasta', N'Pasta with a fresh basil pesto sauce.', 10, 15, 2, N'Easy', N'Boiling', 2, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (61, N'Kimchi Fried Rice', N'Fried rice with spicy kimchi and vegetables.', 15, 10, 2, N'Easy', N'Stir Frying', 3, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (62, N'Braised Short Ribs', N'Tender short ribs cooked in a rich sauce.', 30, 120, 4, N'Hard', N'Stewing', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (63, N'Shrimp Pad See Ew', N'Thai stir-fried noodles with shrimp and vegetables.', 20, 15, 4, N'Medium', N'Stir Frying', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (64, N'Chicken Satay', N'Grilled chicken skewers served with peanut sauce.', 15, 10, 2, N'Easy', N'Grilling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (65, N'Chocolate Mousse', N'Creamy chocolate dessert with whipped cream.', 10, 0, 4, N'Easy', N'Blending', 1, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (66, N'Zucchini Noodles', N'Low-carb noodles made from spiralized zucchini.', 10, 5, 2, N'Easy', N'Raw', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (67, N'Eggplant Parmesan', N'Breaded eggplant baked with marinara and cheese.', 20, 30, 4, N'Medium', N'Baking', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (68, N'Sweet Potato Fries', N'Crispy sweet potato fries baked or fried.', 10, 20, 2, N'Easy', N'Frying', 3, 1, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (69, N'Turkey Sandwich', N'Classic sandwich with turkey, lettuce, and mayo.', 5, 5, 1, N'Easy', N'Raw', 3, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (70, N'Chicken Caesar Wrap', N'Caesar salad wrapped in a tortilla with grilled chicken.', 10, 5, 2, N'Easy', N'Raw', 3, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (71, N'French Onion Soup', N'Rich soup with caramelized onions and cheese-topped bread.', 20, 40, 4, N'Medium', N'Simmering', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (72, N'Black Bean Burrito', N'Burrito filled with black beans, rice, and salsa.', 15, 10, 2, N'Easy', N'Frying', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (73, N'Pumpkin Risotto', N'Creamy risotto made with pumpkin and Parmesan cheese.', 20, 30, 4, N'Medium', N'Simmering', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (74, N'Pho Ga', N'Vietnamese chicken noodle soup.', 30, 60, 4, N'Medium', N'Boiling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (75, N'Chicken Katsu', N'Breaded and fried chicken cutlet served with sauce.', 15, 10, 2, N'Medium', N'Frying', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (76, N'Vegetable Lasagna', N'Lasagna with layers of vegetables and cheese.', 30, 45, 6, N'Medium', N'Baking', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (77, N'Almond Croissant', N'Flaky croissant filled with almond paste.', 15, 20, 4, N'Medium', N'Baking', 1, 2, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (78, N'Grilled Pineapple', N'Sweet grilled pineapple slices with caramelized edges.', 5, 10, 2, N'Easy', N'Grilling', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (79, N'Cucumber Sushi', N'Simple sushi rolls with cucumber and rice.', 15, 0, 4, N'Easy', N'Raw', 3, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (80, N'Paneer Butter Masala', N'Indian curry with paneer in a creamy tomato sauce.', 20, 30, 4, N'Medium', N'Stewing', 2, 1, 1, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (81, N'Egg Salad Sandwich', N'Sandwich with egg salad filling.', 5, 5, 2, N'Easy', N'Raw', 3, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (82, N'Roasted Brussels Sprouts', N'Crispy Brussels sprouts with olive oil and salt.', 10, 20, 2, N'Easy', N'Roasting', 3, 1, 2, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
INSERT [dbo].[recipe] ([recipeId], [title], [description], [preparationTime], [cookingTime], [servings], [difficultyLevel], [cookingMethod], [categoryId], [userId], [originId], [createdDate], [lastUpdated], [activeStatus]) VALUES (83, N'Lamb Shawarma', N'Spiced lamb wrapped in pita bread with veggies.', 20, 30, 2, N'Medium', N'Grilling', 2, 2, 3, CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), 1)
SET IDENTITY_INSERT [dbo].[recipe] OFF
GO
SET IDENTITY_INSERT [dbo].[subscription] ON 

INSERT [dbo].[subscription] ([subscriptionId], [name], [description], [price], [duration], [activeStatus]) VALUES (1, N'Free Plan', N'Access to basic recipes', 0, 30, 1)
INSERT [dbo].[subscription] ([subscriptionId], [name], [description], [price], [duration], [activeStatus]) VALUES (2, N'Pro Plan', N'Access to all recipes and features', 9.99, 30, 1)
INSERT [dbo].[subscription] ([subscriptionId], [name], [description], [price], [duration], [activeStatus]) VALUES (3, N'Premium Plan', N'Unlimited access with exclusive content', 19.99, 30, 1)
SET IDENTITY_INSERT [dbo].[subscription] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([userId], [username], [role], [fullname], [email], [password], [viewedRecipe], [activeStatus], [created_at]) VALUES (1, N'john_doe', N'Admin', N'John Doe', N'john@example.com', N'password123', 15, 1, CAST(N'2024-11-26' AS Date))
INSERT [dbo].[user] ([userId], [username], [role], [fullname], [email], [password], [viewedRecipe], [activeStatus], [created_at]) VALUES (2, N'jane_smith', N'User', N'Jane Smith', N'jane@example.com', N'password123', 10, 1, CAST(N'2024-11-26' AS Date))
INSERT [dbo].[user] ([userId], [username], [role], [fullname], [email], [password], [viewedRecipe], [activeStatus], [created_at]) VALUES (3, N'alice_jones', N'User', N'Alice Jones', N'alice@example.com', N'password123', 5, 1, CAST(N'2024-11-26' AS Date))
INSERT [dbo].[user] ([userId], [username], [role], [fullname], [email], [password], [viewedRecipe], [activeStatus], [created_at]) VALUES (4, N'bob_brown', N'User', N'Bob Brown', N'bob@example.com', N'password123', 20, 1, CAST(N'2024-11-26' AS Date))
SET IDENTITY_INSERT [dbo].[user] OFF
GO
SET IDENTITY_INSERT [dbo].[userSubscription] ON 

INSERT [dbo].[userSubscription] ([userSubscriptionId], [userId], [subscriptionId], [startDate], [endDate], [activeStatus]) VALUES (1, 1, 2, CAST(N'2024-11-26T22:42:28.460' AS DateTime), CAST(N'2024-12-26T22:42:28.460' AS DateTime), 1)
INSERT [dbo].[userSubscription] ([userSubscriptionId], [userId], [subscriptionId], [startDate], [endDate], [activeStatus]) VALUES (2, 2, 3, CAST(N'2024-11-26T22:42:28.460' AS DateTime), CAST(N'2024-12-26T22:42:28.460' AS DateTime), 1)
INSERT [dbo].[userSubscription] ([userSubscriptionId], [userId], [subscriptionId], [startDate], [endDate], [activeStatus]) VALUES (3, 3, 1, CAST(N'2024-11-26T22:42:28.460' AS DateTime), CAST(N'2024-12-26T22:42:28.460' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[userSubscription] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__user__AB6E6164F2A89FA4]    Script Date: 11/26/2024 11:02:16 PM ******/
ALTER TABLE [dbo].[user] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[category] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[ingredient] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[nutritionInfo] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[origin] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[payment] ADD  DEFAULT (getdate()) FOR [paymentDate]
GO
ALTER TABLE [dbo].[payment] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[recipe] ADD  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[recipe] ADD  DEFAULT (getdate()) FOR [lastUpdated]
GO
ALTER TABLE [dbo].[recipe] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[subscription] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT ((0)) FOR [viewedRecipe]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[userSubscription] ADD  DEFAULT ((1)) FOR [activeStatus]
GO
ALTER TABLE [dbo].[ingredient]  WITH CHECK ADD FOREIGN KEY([nutritionInfoId])
REFERENCES [dbo].[nutritionInfo] ([nutritionInfoId])
GO
ALTER TABLE [dbo].[ingredientNutritionInfo]  WITH CHECK ADD FOREIGN KEY([ingredientId])
REFERENCES [dbo].[ingredient] ([ingredientId])
GO
ALTER TABLE [dbo].[ingredientNutritionInfo]  WITH CHECK ADD FOREIGN KEY([nutritionInfoId])
REFERENCES [dbo].[nutritionInfo] ([nutritionInfoId])
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD FOREIGN KEY([userSubscriptionId])
REFERENCES [dbo].[userSubscription] ([userSubscriptionId])
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD FOREIGN KEY([categoryId])
REFERENCES [dbo].[category] ([categoryId])
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD FOREIGN KEY([originId])
REFERENCES [dbo].[origin] ([originId])
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
ALTER TABLE [dbo].[recipeIngredient]  WITH CHECK ADD FOREIGN KEY([ingredientId])
REFERENCES [dbo].[ingredient] ([ingredientId])
GO
ALTER TABLE [dbo].[recipeIngredient]  WITH CHECK ADD FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipe] ([recipeId])
GO
ALTER TABLE [dbo].[step]  WITH CHECK ADD FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipe] ([recipeId])
GO
ALTER TABLE [dbo].[userSubscription]  WITH CHECK ADD FOREIGN KEY([subscriptionId])
REFERENCES [dbo].[subscription] ([subscriptionId])
GO
ALTER TABLE [dbo].[userSubscription]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
USE [master]
GO
ALTER DATABASE [RecipeGiver] SET  READ_WRITE 
GO
