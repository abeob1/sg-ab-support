-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jan 05, 2017 at 11:42 AM
-- Server version: 10.1.16-MariaDB
-- PHP Version: 5.6.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `help`
--

-- --------------------------------------------------------

--
-- Table structure for table `banlist`
--

CREATE TABLE `banlist` (
  `id` int(10) UNSIGNED NOT NULL,
  `ban_status` tinyint(1) NOT NULL,
  `email_address` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `internal_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `canned_response`
--

CREATE TABLE `canned_response` (
  `id` int(10) UNSIGNED NOT NULL,
  `user_id` int(10) UNSIGNED NOT NULL,
  `title` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `message` text COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `custom_forms`
--

CREATE TABLE `custom_forms` (
  `id` int(10) UNSIGNED NOT NULL,
  `formname` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `custom_form_fields`
--

CREATE TABLE `custom_form_fields` (
  `id` int(10) UNSIGNED NOT NULL,
  `forms_id` int(11) NOT NULL,
  `label` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `value` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `required` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `date_format`
--

CREATE TABLE `date_format` (
  `id` int(10) UNSIGNED NOT NULL,
  `format` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `date_time_format`
--

CREATE TABLE `date_time_format` (
  `id` int(10) UNSIGNED NOT NULL,
  `format` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sla` int(10) UNSIGNED DEFAULT NULL,
  `manager` int(10) UNSIGNED DEFAULT NULL,
  `ticket_assignment` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `outgoing_email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `template_set` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `auto_ticket_response` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `auto_message_response` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `auto_response_email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `recipient` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `group_access` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `department_sign` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`id`, `name`, `type`, `sla`, `manager`, `ticket_assignment`, `outgoing_email`, `template_set`, `auto_ticket_response`, `auto_message_response`, `auto_response_email`, `recipient`, `group_access`, `department_sign`, `created_at`, `updated_at`) VALUES
(1, 'Support', 'Support', NULL, NULL, '', '', '', '', '', '', '', '', '', '2016-12-29 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `emails`
--

CREATE TABLE `emails` (
  `id` int(10) UNSIGNED NOT NULL,
  `email_address` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `department` int(10) UNSIGNED DEFAULT NULL,
  `priority` int(10) UNSIGNED DEFAULT NULL,
  `help_topic` int(10) UNSIGNED DEFAULT NULL,
  `user_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `fetching_host` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `fetching_port` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `fetching_protocol` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `fetching_encryption` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `mailbox_protocol` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `imap_config` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `folder` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sending_host` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sending_port` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sending_protocol` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sending_encryption` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `smtp_validate` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `smtp_authentication` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `internal_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `auto_response` tinyint(1) NOT NULL,
  `fetching_status` tinyint(1) NOT NULL,
  `move_to_folder` tinyint(1) NOT NULL,
  `delete_email` tinyint(1) NOT NULL,
  `do_nothing` tinyint(1) NOT NULL,
  `sending_status` tinyint(1) NOT NULL,
  `authentication` tinyint(1) NOT NULL,
  `header_spoofing` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `group_status` tinyint(1) NOT NULL,
  `can_create_ticket` tinyint(1) NOT NULL,
  `can_edit_ticket` tinyint(1) NOT NULL,
  `can_post_ticket` tinyint(1) NOT NULL,
  `can_close_ticket` tinyint(1) NOT NULL,
  `can_assign_ticket` tinyint(1) NOT NULL,
  `can_transfer_ticket` tinyint(1) NOT NULL,
  `can_delete_ticket` tinyint(1) NOT NULL,
  `can_ban_email` tinyint(1) NOT NULL,
  `can_manage_canned` tinyint(1) NOT NULL,
  `can_manage_faq` tinyint(1) NOT NULL,
  `can_view_agent_stats` tinyint(1) NOT NULL,
  `department_access` tinyint(1) NOT NULL,
  `admin_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `group_assign_department`
--

CREATE TABLE `group_assign_department` (
  `id` int(10) UNSIGNED NOT NULL,
  `group_id` int(10) UNSIGNED NOT NULL,
  `department_id` int(10) UNSIGNED NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `help_topic`
--

CREATE TABLE `help_topic` (
  `id` int(10) UNSIGNED NOT NULL,
  `topic` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `parent_topic` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `custom_form` int(10) UNSIGNED DEFAULT NULL,
  `department` int(10) UNSIGNED DEFAULT NULL,
  `ticket_status` int(10) UNSIGNED DEFAULT NULL,
  `priority` int(10) UNSIGNED DEFAULT NULL,
  `sla_plan` int(10) UNSIGNED DEFAULT NULL,
  `thank_page` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ticket_num_format` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `internal_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `type` tinyint(1) NOT NULL,
  `auto_assign` int(10) UNSIGNED DEFAULT NULL,
  `auto_response` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `help_topic`
--

INSERT INTO `help_topic` (`id`, `topic`, `parent_topic`, `custom_form`, `department`, `ticket_status`, `priority`, `sla_plan`, `thank_page`, `ticket_num_format`, `internal_notes`, `status`, `type`, `auto_assign`, `auto_response`, `created_at`, `updated_at`) VALUES
(1, 'SAP B1', '', NULL, 1, NULL, 1, NULL, '', '', '', 1, 0, NULL, 0, '2016-12-29 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `kb_article`
--

CREATE TABLE `kb_article` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` text COLLATE utf8_unicode_ci NOT NULL,
  `slug` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `description` text COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `type` tinyint(1) NOT NULL,
  `publish_time` datetime DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kb_article_relationship`
--

CREATE TABLE `kb_article_relationship` (
  `id` int(10) UNSIGNED NOT NULL,
  `article_id` int(10) UNSIGNED NOT NULL,
  `category_id` int(10) UNSIGNED NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kb_category`
--

CREATE TABLE `kb_category` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `slug` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `description` text COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `parent` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kb_comment`
--

CREATE TABLE `kb_comment` (
  `id` int(10) UNSIGNED NOT NULL,
  `article_id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `website` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `comment` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kb_pages`
--

CREATE TABLE `kb_pages` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `visibility` tinyint(1) NOT NULL,
  `slug` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `description` text COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kb_settings`
--

CREATE TABLE `kb_settings` (
  `id` int(10) UNSIGNED NOT NULL,
  `pagination` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `languages`
--

CREATE TABLE `languages` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `locale` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `log_notification`
--

CREATE TABLE `log_notification` (
  `id` int(10) UNSIGNED NOT NULL,
  `log` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mailbox_protocol`
--

CREATE TABLE `mailbox_protocol` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `value` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `migrations`
--

CREATE TABLE `migrations` (
  `migration` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `migrations`
--

INSERT INTO `migrations` (`migration`, `batch`) VALUES
('2016_02_16_140450_create_banlist_table', 1),
('2016_02_16_140450_create_canned_response_table', 1),
('2016_02_16_140450_create_custom_form_fields_table', 1),
('2016_02_16_140450_create_custom_forms_table', 1),
('2016_02_16_140450_create_date_format_table', 1),
('2016_02_16_140450_create_date_time_format_table', 1),
('2016_02_16_140450_create_department_table', 1),
('2016_02_16_140450_create_emails_table', 1),
('2016_02_16_140450_create_group_assign_department_table', 1),
('2016_02_16_140450_create_groups_table', 1),
('2016_02_16_140450_create_help_topic_table', 1),
('2016_02_16_140450_create_kb_article_relationship_table', 1),
('2016_02_16_140450_create_kb_article_table', 1),
('2016_02_16_140450_create_kb_category_table', 1),
('2016_02_16_140450_create_kb_comment_table', 1),
('2016_02_16_140450_create_kb_pages_table', 1),
('2016_02_16_140450_create_kb_settings_table', 1),
('2016_02_16_140450_create_languages_table', 1),
('2016_02_16_140450_create_log_notification_table', 1),
('2016_02_16_140450_create_mailbox_protocol_table', 1),
('2016_02_16_140450_create_organization_table', 1),
('2016_02_16_140450_create_password_resets_table', 1),
('2016_02_16_140450_create_plugins_table', 1),
('2016_02_16_140450_create_settings_alert_notice_table', 1),
('2016_02_16_140450_create_settings_auto_response_table', 1),
('2016_02_16_140450_create_settings_company_table', 1),
('2016_02_16_140450_create_settings_email_table', 1),
('2016_02_16_140450_create_settings_ratings_table', 1),
('2016_02_16_140450_create_settings_system_table', 1),
('2016_02_16_140450_create_settings_ticket_table', 1),
('2016_02_16_140450_create_sla_plan_table', 1),
('2016_02_16_140450_create_team_assign_agent_table', 1),
('2016_02_16_140450_create_teams_table', 1),
('2016_02_16_140450_create_template_table', 1),
('2016_02_16_140450_create_ticket_attachment_table', 1),
('2016_02_16_140450_create_ticket_collaborator_table', 1),
('2016_02_16_140450_create_ticket_form_data_table', 1),
('2016_02_16_140450_create_ticket_priority_table', 1),
('2016_02_16_140450_create_ticket_source_table', 1),
('2016_02_16_140450_create_ticket_status_table', 1),
('2016_02_16_140450_create_ticket_thread_table', 1),
('2016_02_16_140450_create_tickets_table', 1),
('2016_02_16_140450_create_time_format_table', 1),
('2016_02_16_140450_create_timezone_table', 1),
('2016_02_16_140450_create_user_assign_organization_table', 1),
('2016_02_16_140450_create_users_table', 1),
('2016_02_16_140450_create_version_check_table', 1),
('2016_02_16_140450_create_widgets_table', 1),
('2016_02_16_140454_add_foreign_keys_to_canned_response_table', 1);

-- --------------------------------------------------------

--
-- Table structure for table `organization`
--

CREATE TABLE `organization` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `website` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `address` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `head` int(10) UNSIGNED DEFAULT NULL,
  `internal_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `organization`
--

INSERT INTO `organization` (`id`, `name`, `phone`, `website`, `address`, `head`, `internal_notes`, `created_at`, `updated_at`) VALUES
(1, 'Abeo-electra', '9899898989', 'www.abeo-electra.com', '102, south street , sg', NULL, '', '2016-11-28 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `password_resets`
--

CREATE TABLE `password_resets` (
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `token` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` datetime NOT NULL DEFAULT '0000-00-00 00:00:00'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `plugins`
--

CREATE TABLE `plugins` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `path` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_alert_notice`
--

CREATE TABLE `settings_alert_notice` (
  `id` int(10) UNSIGNED NOT NULL,
  `ticket_status` tinyint(1) NOT NULL,
  `ticket_admin_email` tinyint(1) NOT NULL,
  `ticket_department_manager` tinyint(1) NOT NULL,
  `ticket_department_member` tinyint(1) NOT NULL,
  `ticket_organization_accmanager` tinyint(1) NOT NULL,
  `message_status` tinyint(1) NOT NULL,
  `message_last_responder` tinyint(1) NOT NULL,
  `message_assigned_agent` tinyint(1) NOT NULL,
  `message_department_manager` tinyint(1) NOT NULL,
  `message_organization_accmanager` tinyint(1) NOT NULL,
  `internal_status` tinyint(1) NOT NULL,
  `internal_last_responder` tinyint(1) NOT NULL,
  `internal_assigned_agent` tinyint(1) NOT NULL,
  `internal_department_manager` tinyint(1) NOT NULL,
  `assignment_status` tinyint(1) NOT NULL,
  `assignment_assigned_agent` tinyint(1) NOT NULL,
  `assignment_team_leader` tinyint(1) NOT NULL,
  `assignment_team_member` tinyint(1) NOT NULL,
  `transfer_status` tinyint(1) NOT NULL,
  `transfer_assigned_agent` tinyint(1) NOT NULL,
  `transfer_department_manager` tinyint(1) NOT NULL,
  `transfer_department_member` tinyint(1) NOT NULL,
  `overdue_status` tinyint(1) NOT NULL,
  `overdue_assigned_agent` tinyint(1) NOT NULL,
  `overdue_department_manager` tinyint(1) NOT NULL,
  `overdue_department_member` tinyint(1) NOT NULL,
  `system_error` tinyint(1) NOT NULL,
  `sql_error` tinyint(1) NOT NULL,
  `excessive_failure` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_auto_response`
--

CREATE TABLE `settings_auto_response` (
  `id` int(10) UNSIGNED NOT NULL,
  `new_ticket` tinyint(1) NOT NULL,
  `agent_new_ticket` tinyint(1) NOT NULL,
  `submitter` tinyint(1) NOT NULL,
  `participants` tinyint(1) NOT NULL,
  `overlimit` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_company`
--

CREATE TABLE `settings_company` (
  `id` int(10) UNSIGNED NOT NULL,
  `company_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `website` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `address` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `landing_page` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `offline_page` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `thank_page` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `logo` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `use_logo` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_email`
--

CREATE TABLE `settings_email` (
  `id` int(10) UNSIGNED NOT NULL,
  `template` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sys_email` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `alert_email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `admin_email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `mta` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `email_fetching` tinyint(1) NOT NULL,
  `notification_cron` tinyint(1) NOT NULL,
  `strip` tinyint(1) NOT NULL,
  `separator` tinyint(1) NOT NULL,
  `all_emails` tinyint(1) NOT NULL,
  `email_collaborator` tinyint(1) NOT NULL,
  `attachment` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_ratings`
--

CREATE TABLE `settings_ratings` (
  `id` int(10) UNSIGNED NOT NULL,
  `rating_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `publish` int(11) NOT NULL,
  `modify` int(11) NOT NULL,
  `slug` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_system`
--

CREATE TABLE `settings_system` (
  `id` int(10) UNSIGNED NOT NULL,
  `status` tinyint(1) NOT NULL,
  `url` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `department` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `page_size` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `log_level` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `purge_log` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `api_enable` int(11) NOT NULL,
  `api_key_mandatory` int(11) NOT NULL,
  `api_key` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `name_format` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `time_farmat` int(10) UNSIGNED DEFAULT NULL,
  `date_format` int(10) UNSIGNED DEFAULT NULL,
  `date_time_format` int(10) UNSIGNED DEFAULT NULL,
  `day_date_time` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `time_zone` int(10) UNSIGNED DEFAULT NULL,
  `content` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `version` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `settings_ticket`
--

CREATE TABLE `settings_ticket` (
  `id` int(10) UNSIGNED NOT NULL,
  `num_format` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `num_sequence` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `priority` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `sla` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `help_topic` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `max_open_ticket` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `collision_avoid` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `lock_ticket_frequency` varchar(255) COLLATE utf8_unicode_ci NOT NULL DEFAULT '0',
  `captcha` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `claim_response` tinyint(1) NOT NULL,
  `assigned_ticket` tinyint(1) NOT NULL,
  `answered_ticket` tinyint(1) NOT NULL,
  `agent_mask` tinyint(1) NOT NULL,
  `html` tinyint(1) NOT NULL,
  `client_update` tinyint(1) NOT NULL,
  `max_file_size` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `sla_plan`
--

CREATE TABLE `sla_plan` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `grace_period` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `admin_note` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `transient` tinyint(1) NOT NULL,
  `ticket_overdue` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `teams`
--

CREATE TABLE `teams` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `team_lead` int(10) UNSIGNED DEFAULT NULL,
  `assign_alert` tinyint(1) NOT NULL,
  `admin_notes` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team_assign_agent`
--

CREATE TABLE `team_assign_agent` (
  `id` int(10) UNSIGNED NOT NULL,
  `team_id` int(10) UNSIGNED DEFAULT NULL,
  `agent_id` int(10) UNSIGNED DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `template`
--

CREATE TABLE `template` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  `template_set_to_clone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `language` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `internal_note` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tickets`
--

CREATE TABLE `tickets` (
  `id` int(10) UNSIGNED NOT NULL,
  `ticket_number` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `user_id` int(10) UNSIGNED DEFAULT NULL,
  `dept_id` int(10) UNSIGNED DEFAULT NULL,
  `team_id` int(10) UNSIGNED DEFAULT NULL,
  `priority_id` int(10) UNSIGNED DEFAULT NULL,
  `sla` int(10) UNSIGNED DEFAULT NULL,
  `help_topic_id` int(10) UNSIGNED DEFAULT NULL,
  `status` int(10) UNSIGNED DEFAULT NULL,
  `rating` tinyint(1) NOT NULL,
  `ratingreply` tinyint(1) NOT NULL,
  `flags` int(11) NOT NULL,
  `ip_address` int(11) NOT NULL,
  `assigned_to` int(10) UNSIGNED DEFAULT NULL,
  `lock_by` int(11) NOT NULL,
  `lock_at` datetime DEFAULT NULL,
  `source` int(10) UNSIGNED DEFAULT NULL,
  `isoverdue` int(11) NOT NULL,
  `reopened` int(11) NOT NULL,
  `isanswered` int(11) NOT NULL,
  `html` int(11) NOT NULL,
  `is_deleted` int(11) NOT NULL,
  `closed` int(11) NOT NULL,
  `is_transferred` tinyint(1) NOT NULL,
  `transferred_at` datetime NOT NULL,
  `reopened_at` datetime DEFAULT NULL,
  `duedate` datetime DEFAULT NULL,
  `closed_at` datetime DEFAULT NULL,
  `last_message_at` datetime DEFAULT NULL,
  `last_response_at` datetime DEFAULT NULL,
  `approval` int(11) NOT NULL,
  `follow_up` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `Scenario` text COLLATE utf8_unicode_ci NOT NULL,
  `ExpectedScenario` text COLLATE utf8_unicode_ci NOT NULL,
  `ActualScenario` text COLLATE utf8_unicode_ci NOT NULL,
  `Subject` text COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `tickets`
--

INSERT INTO `tickets` (`id`, `ticket_number`, `user_id`, `dept_id`, `team_id`, `priority_id`, `sla`, `help_topic_id`, `status`, `rating`, `ratingreply`, `flags`, `ip_address`, `assigned_to`, `lock_by`, `lock_at`, `source`, `isoverdue`, `reopened`, `isanswered`, `html`, `is_deleted`, `closed`, `is_transferred`, `transferred_at`, `reopened_at`, `duedate`, `closed_at`, `last_message_at`, `last_response_at`, `approval`, `follow_up`, `created_at`, `updated_at`, `Scenario`, `ExpectedScenario`, `ActualScenario`, `Subject`) VALUES
(1, 'AAA1', 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, NULL, 0, NULL, 1, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, 'Scenario', 'ExpectedScenario	', 'ActualScenario', ''),
(12, 'AAA12', 17, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '0000-00-00 00:00:00', NULL, 'dd', 'ff', 'cdd', ''),
(20, 'AAA13', 5, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '0000-00-00 00:00:00', NULL, 'fdkfj', 'kcjfcj', 'jdjf', ''),
(21, 'AAA14', 5, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '0000-00-00 00:00:00', NULL, 'scenario', 'scenario', 'scenario', 'test subject'),
(22, 'AAA15', 9, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 8, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '0000-00-00 00:00:00', '2017-01-02 00:19:06', 'scenario', 'scenario', 'scenario', 'test subject'),
(23, 'AAA16', 5, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '2016-12-29 21:44:23', NULL, 'scenario', 'scenario', 'scenario', 'test subject'),
(24, 'AAA17', 5, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '0000-00-00 00:00:00', NULL, 'scenario', 'scenario', 'scenario', 'test subject'),
(25, 'AAA18', 9, 0, 0, 1, NULL, 1, 2, 0, 0, 0, 0, 2, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '2016-12-29 22:22:54', '2017-01-02 22:55:24', 'scenario', 'scenario', 'scenario', 'test subject'),
(26, 'AAA19', 9, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '2017-01-02 23:08:42', NULL, 'the ok button need to change , replace submit button with ok button.', 'the ok button need to change , replace submit button with ok button.', 'ok button in wrong place .', 'Button need to change'),
(27, 'AAA20', 9, 0, 0, 1, NULL, 1, 1, 0, 0, 0, 0, 12, 0, NULL, 1, 0, 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL, 0, 0, '2017-01-04 22:54:57', '2017-01-04 23:03:52', 'Purchase order need to create, followed by delivery order and followed by invoice,', 'User need to create purchase order and delivery order before invoice', 'But in SAP user can create invoice without delivery order', 'Purchase order need to create');

-- --------------------------------------------------------

--
-- Table structure for table `ticket_attachment`
--

CREATE TABLE `ticket_attachment` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `thread_id` int(10) UNSIGNED DEFAULT NULL,
  `size` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `poster` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `file` mediumblob
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ticket_collaborator`
--

CREATE TABLE `ticket_collaborator` (
  `id` int(10) UNSIGNED NOT NULL,
  `isactive` tinyint(1) NOT NULL,
  `ticket_id` int(10) UNSIGNED DEFAULT NULL,
  `user_id` int(10) UNSIGNED DEFAULT NULL,
  `role` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ticket_form_data`
--

CREATE TABLE `ticket_form_data` (
  `id` int(10) UNSIGNED NOT NULL,
  `ticket_id` int(10) UNSIGNED DEFAULT NULL,
  `title` text COLLATE utf8_unicode_ci NOT NULL,
  `content` text COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ticket_priority`
--

CREATE TABLE `ticket_priority` (
  `priority_id` int(10) UNSIGNED NOT NULL,
  `priority` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `status` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `priority_desc` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `priority_color` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `priority_urgency` tinyint(1) NOT NULL,
  `ispublic` tinyint(1) NOT NULL,
  `is_default` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ticket_priority`
--

INSERT INTO `ticket_priority` (`priority_id`, `priority`, `status`, `priority_desc`, `priority_color`, `priority_urgency`, `ispublic`, `is_default`, `created_at`, `updated_at`) VALUES
(1, 'critical', 'active', 'critical change or need to do now', '#FF0000', 0, 1, 'no', '2016-12-28 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `ticket_source`
--

CREATE TABLE `ticket_source` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `value` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ticket_source`
--

INSERT INTO `ticket_source` (`id`, `name`, `value`) VALUES
(1, 'Login Panel', 'Ticket created from login page');

-- --------------------------------------------------------

--
-- Table structure for table `ticket_status`
--

CREATE TABLE `ticket_status` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `state` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `mode` int(11) NOT NULL,
  `message` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `flags` int(11) NOT NULL,
  `sort` int(11) NOT NULL,
  `email_user` int(11) NOT NULL,
  `icon_class` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `properties` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ticket_status`
--

INSERT INTO `ticket_status` (`id`, `name`, `state`, `mode`, `message`, `flags`, `sort`, `email_user`, `icon_class`, `properties`, `created_at`, `updated_at`) VALUES
(1, 'Open', 'open', 0, '', 0, 0, 0, 'fa fa-folder-open-o', '', '2016-12-29 18:30:00', NULL),
(2, 'Closed', 'Closed', 0, '', 0, 0, 0, 'fa fa-check', '', '2017-01-01 18:30:00', NULL),
(3, 'Resolved ', 'Resolved ', 0, '', 0, 0, 0, 'fa fa-check-circle', '', '2017-01-01 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `ticket_thread`
--

CREATE TABLE `ticket_thread` (
  `id` int(10) UNSIGNED NOT NULL,
  `ticket_id` int(10) UNSIGNED DEFAULT NULL,
  `user_id` int(10) UNSIGNED DEFAULT NULL,
  `poster` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `source` int(10) UNSIGNED DEFAULT NULL,
  `reply_rating` int(11) NOT NULL,
  `rating_count` int(11) NOT NULL,
  `is_internal` tinyint(1) NOT NULL,
  `title` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `bodyText` text COLLATE utf8_unicode_ci,
  `format` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ip_address` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `ticket_thread`
--

INSERT INTO `ticket_thread` (`id`, `ticket_id`, `user_id`, `poster`, `source`, `reply_rating`, `rating_count`, `is_internal`, `title`, `bodyText`, `format`, `ip_address`, `created_at`, `updated_at`) VALUES
(1, 1, 1, '', 1, 0, 0, 0, 'we are working on this', '\n================================================================================\n= README.TXT                                                                   =\n================================================================================\n\nInstallation instructions:\n--------------------------\n\nFaveo HELPDESK installation should not take more than 15 minutes to complete.\nDetails are provided in this article:\n\nhttp://ladybirdweb.com/support/knowledgebase\n\nUpgrade instructions:\n---------------------\n\nIf you are upgrading your Faveo HELPDESK to the latest stable release, please\nfollow the instructions provided in this article:\n\nhttp://ladybirdweb.com/support/knowledgebase\n\n================================================================================\n\nThank you for choosing Faveo HELPDESK! We hope that you will enjoy using it as\nmuch as we enjoy developing it!\n', '', '', '2016-12-29 18:30:00', NULL),
(3, 1, 1, '', 2, 0, 0, 0, '', '<p>fsdfgsdfg</p>', '', '::1', '2016-12-30 06:28:00', NULL),
(4, 22, 1, '', 2, 0, 0, 0, '', '<p>Hi all</p><p>this is test msg&nbsp;</p><p>thanks</p><p>thomas</p>', '', '::1', '2016-12-31 01:59:33', NULL),
(5, 22, 1, '', 2, 0, 0, 0, '', '<p>Hi my dear&nbsp;</p><p>jfjdwnfsj kjd&#39; &#39;kjsaldflkadsf &#39;&#39;&#39;asdfasdfawfd&quot;sdfasdf&quot;asdfasdfasdf</p><p>thanks</p><p><strong>thomas</strong></p>', '', '::1', '2016-12-31 02:11:52', NULL),
(6, 25, 1, '', 2, 0, 0, 0, '', '<p>yfdhdfhdfh</p>', '', '::1', '2017-01-02 23:06:17', NULL),
(7, 26, 1, '', 2, 0, 0, 0, '', '<p>Hi sir .,</p><p>We are working on this and we will update you asap .</p><p>Thanks</p><p>Support Team</p>', '', '::1', '2017-01-03 23:53:20', NULL),
(8, 27, 1, '', 2, 0, 0, 0, '', '<p>Hi .,</p><p>We are working on this , and we will update you asap .,</p><p>Thanks</p><p>Thomas</p>', '', '::1', '2017-01-04 23:01:59', NULL),
(9, 27, 1, '', 2, 0, 0, 0, '', '<p>Hi .,</p><p>We found the problem , And we will fix this asap</p><p>thanks</p><p>thomas</p>', '', '::1', '2017-01-04 23:06:53', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `timezone`
--

CREATE TABLE `timezone` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `location` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `time_format`
--

CREATE TABLE `time_format` (
  `id` int(10) UNSIGNED NOT NULL,
  `format` varchar(255) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(10) UNSIGNED NOT NULL,
  `user_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `first_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `last_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `gender` tinyint(1) NOT NULL,
  `email` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ban` tinyint(1) NOT NULL,
  `password` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `active` int(11) NOT NULL,
  `ext` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `country_code` int(11) NOT NULL,
  `phone_number` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `mobile` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `agent_sign` text COLLATE utf8_unicode_ci NOT NULL,
  `account_type` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `account_status` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `assign_group` int(10) UNSIGNED DEFAULT NULL,
  `primary_dpt` int(10) UNSIGNED DEFAULT NULL,
  `agent_tzone` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `daylight_save` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `limit_access` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `directory_listing` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `vacation_mode` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `company` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `role` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `internal_note` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `profile_pic` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `remember_token` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `user_name`, `first_name`, `last_name`, `gender`, `email`, `ban`, `password`, `active`, `ext`, `country_code`, `phone_number`, `mobile`, `agent_sign`, `account_type`, `account_status`, `assign_group`, `primary_dpt`, `agent_tzone`, `daylight_save`, `limit_access`, `directory_listing`, `vacation_mode`, `company`, `role`, `internal_note`, `profile_pic`, `remember_token`, `created_at`, `updated_at`) VALUES
(1, 'admin', 'thomas', 'albert', 1, 'admin@demo.com', 1, '0192023a7bbd73250516f069df18b500', 1, '', 91, '9566813140', '9566813140', '', 'Admin', 'active', NULL, NULL, '', '', '', '', '', '1', '', '', '', NULL, '2016-12-14 20:30:00', '2017-01-04 06:58:30'),
(2, 'testuser', 'test', 'test', 1, 'test@test.com', 0, '0192023a7bbd73250516f069df18b500', 0, '', 0, '', NULL, 'dd', 'Agent', '', NULL, 1, '', '', '', '', '', '', '', '', '', NULL, NULL, '2017-01-04 06:58:38'),
(3, 'albert', 'albert', 'welcome', 1, 'albert@gmail.com', 0, 'sss', 1, '', 0, '', NULL, '', 'Agent', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2016-11-28 18:30:00', '2017-01-03 20:08:47'),
(5, 'thomas', '', '', 1, 'thomas@gmail.com', 0, '383e8048f3ea9c5146db674eaf04a214', 1, '', 0, '', NULL, '', 'Customer', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '0000-00-00 00:00:00', '2017-01-04 05:27:43'),
(8, 'Isha', 'Thomas', 'Albert', 1, 'ishathom@gmail.com', 0, '1234', 1, '', 0, '9566813142', '9566813142', '', 'Customer', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2017-01-01 18:30:00', '2017-01-04 23:11:44'),
(9, 'thomas', '', '', 1, 'customer@demo.com', 0, '81dc9bdb52d04dc20036dbd8313ed055', 1, '', 0, '', NULL, '', 'Customer', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2017-01-01 18:30:00', '2017-01-03 20:14:21'),
(10, '', '', '', 1, '', 0, '4509d4b766cd7a87b6b14cdf7671c42e', 0, '', 0, '', NULL, '', 'Customer', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2001-03-16 18:30:00', '2017-01-04 07:06:47'),
(12, 'Jeeva Abeo', 'Jeeva', 'Abeo', 1, 'agent@demo.com', 0, '81dc9bdb52d04dc20036dbd8313ed055', 0, '', 91, '9788787878', NULL, '', 'Agent', 'Inactive', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2017-01-03 05:55:03', '2017-01-04 23:11:21'),
(13, 'thomas', '', '', 1, 'thomasp@abeo-electra.com', 0, '87073d9482c1e8c368f6b4658ee90491', 1, '', 0, '', NULL, '', '', 'active', NULL, 1, '', '', '', '', '', '1', '', '', '', NULL, '2001-05-16 18:30:00', '2017-01-04 23:11:25');

-- --------------------------------------------------------

--
-- Table structure for table `user_assign_organization`
--

CREATE TABLE `user_assign_organization` (
  `id` int(10) UNSIGNED NOT NULL,
  `org_id` int(10) UNSIGNED DEFAULT NULL,
  `user_id` int(10) UNSIGNED DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `user_assign_organization`
--

INSERT INTO `user_assign_organization` (`id`, `org_id`, `user_id`, `created_at`, `updated_at`) VALUES
(1, 1, 1, '2016-11-28 18:30:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `version_check`
--

CREATE TABLE `version_check` (
  `id` int(10) UNSIGNED NOT NULL,
  `current_version` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `new_version` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `widgets`
--

CREATE TABLE `widgets` (
  `id` int(11) NOT NULL,
  `name` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `title` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `value` text COLLATE utf8_unicode_ci,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `banlist`
--
ALTER TABLE `banlist`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `canned_response`
--
ALTER TABLE `canned_response`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `custom_forms`
--
ALTER TABLE `custom_forms`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `custom_form_fields`
--
ALTER TABLE `custom_form_fields`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `date_format`
--
ALTER TABLE `date_format`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `date_time_format`
--
ALTER TABLE `date_time_format`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`id`),
  ADD KEY `sla` (`sla`),
  ADD KEY `manager_2` (`manager`);

--
-- Indexes for table `emails`
--
ALTER TABLE `emails`
  ADD PRIMARY KEY (`id`),
  ADD KEY `department` (`department`,`priority`,`help_topic`),
  ADD KEY `department_2` (`department`,`priority`,`help_topic`),
  ADD KEY `priority` (`priority`),
  ADD KEY `help_topic` (`help_topic`);

--
-- Indexes for table `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `group_assign_department`
--
ALTER TABLE `group_assign_department`
  ADD PRIMARY KEY (`id`),
  ADD KEY `group_id` (`group_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `help_topic`
--
ALTER TABLE `help_topic`
  ADD PRIMARY KEY (`id`),
  ADD KEY `custom_form` (`custom_form`),
  ADD KEY `department` (`department`),
  ADD KEY `ticket_status` (`ticket_status`),
  ADD KEY `priority` (`priority`),
  ADD KEY `sla_plan` (`sla_plan`),
  ADD KEY `auto_assign_2` (`auto_assign`);

--
-- Indexes for table `kb_article`
--
ALTER TABLE `kb_article`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kb_article_relationship`
--
ALTER TABLE `kb_article_relationship`
  ADD PRIMARY KEY (`id`),
  ADD KEY `article_relationship_article_id_foreign` (`article_id`),
  ADD KEY `article_relationship_category_id_foreign` (`category_id`);

--
-- Indexes for table `kb_category`
--
ALTER TABLE `kb_category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kb_comment`
--
ALTER TABLE `kb_comment`
  ADD PRIMARY KEY (`id`),
  ADD KEY `comment_article_id_foreign` (`article_id`);

--
-- Indexes for table `kb_pages`
--
ALTER TABLE `kb_pages`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kb_settings`
--
ALTER TABLE `kb_settings`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `languages`
--
ALTER TABLE `languages`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `log_notification`
--
ALTER TABLE `log_notification`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mailbox_protocol`
--
ALTER TABLE `mailbox_protocol`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `organization`
--
ALTER TABLE `organization`
  ADD PRIMARY KEY (`id`),
  ADD KEY `head` (`head`);

--
-- Indexes for table `password_resets`
--
ALTER TABLE `password_resets`
  ADD KEY `password_resets_email_index` (`email`),
  ADD KEY `password_resets_token_index` (`token`);

--
-- Indexes for table `plugins`
--
ALTER TABLE `plugins`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `settings_alert_notice`
--
ALTER TABLE `settings_alert_notice`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `settings_auto_response`
--
ALTER TABLE `settings_auto_response`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `settings_company`
--
ALTER TABLE `settings_company`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `settings_email`
--
ALTER TABLE `settings_email`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `settings_ratings`
--
ALTER TABLE `settings_ratings`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `settings_ratings_slug_unique` (`slug`);

--
-- Indexes for table `settings_system`
--
ALTER TABLE `settings_system`
  ADD PRIMARY KEY (`id`),
  ADD KEY `time_farmat` (`time_farmat`),
  ADD KEY `date_format` (`date_format`),
  ADD KEY `date_time_format` (`date_time_format`),
  ADD KEY `time_zone` (`time_zone`);

--
-- Indexes for table `settings_ticket`
--
ALTER TABLE `settings_ticket`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sla_plan`
--
ALTER TABLE `sla_plan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `teams`
--
ALTER TABLE `teams`
  ADD PRIMARY KEY (`id`),
  ADD KEY `team_lead` (`team_lead`);

--
-- Indexes for table `team_assign_agent`
--
ALTER TABLE `team_assign_agent`
  ADD PRIMARY KEY (`id`),
  ADD KEY `team_id` (`team_id`),
  ADD KEY `agent_id` (`agent_id`);

--
-- Indexes for table `template`
--
ALTER TABLE `template`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tickets`
--
ALTER TABLE `tickets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `dept_id` (`dept_id`),
  ADD KEY `team_id` (`team_id`),
  ADD KEY `priority_id` (`priority_id`),
  ADD KEY `sla` (`sla`),
  ADD KEY `help_topic_id` (`help_topic_id`),
  ADD KEY `status` (`status`),
  ADD KEY `assigned_to` (`assigned_to`),
  ADD KEY `source` (`source`);

--
-- Indexes for table `ticket_attachment`
--
ALTER TABLE `ticket_attachment`
  ADD PRIMARY KEY (`id`),
  ADD KEY `thread_id` (`thread_id`);

--
-- Indexes for table `ticket_collaborator`
--
ALTER TABLE `ticket_collaborator`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ticket_id` (`ticket_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `ticket_form_data`
--
ALTER TABLE `ticket_form_data`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ticket_id` (`ticket_id`);

--
-- Indexes for table `ticket_priority`
--
ALTER TABLE `ticket_priority`
  ADD PRIMARY KEY (`priority_id`);

--
-- Indexes for table `ticket_source`
--
ALTER TABLE `ticket_source`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `ticket_status`
--
ALTER TABLE `ticket_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `ticket_thread`
--
ALTER TABLE `ticket_thread`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ticket_id_2` (`ticket_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `source` (`source`);

--
-- Indexes for table `timezone`
--
ALTER TABLE `timezone`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `time_format`
--
ALTER TABLE `time_format`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `users_email_unique` (`email`),
  ADD UNIQUE KEY `users_mobile_unique` (`mobile`),
  ADD KEY `assign_group_3` (`assign_group`),
  ADD KEY `primary_dpt_2` (`primary_dpt`);

--
-- Indexes for table `user_assign_organization`
--
ALTER TABLE `user_assign_organization`
  ADD PRIMARY KEY (`id`),
  ADD KEY `org_id` (`org_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `version_check`
--
ALTER TABLE `version_check`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `widgets`
--
ALTER TABLE `widgets`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `banlist`
--
ALTER TABLE `banlist`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `canned_response`
--
ALTER TABLE `canned_response`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `custom_forms`
--
ALTER TABLE `custom_forms`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `custom_form_fields`
--
ALTER TABLE `custom_form_fields`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `date_format`
--
ALTER TABLE `date_format`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `date_time_format`
--
ALTER TABLE `date_time_format`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `department`
--
ALTER TABLE `department`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `emails`
--
ALTER TABLE `emails`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `groups`
--
ALTER TABLE `groups`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `group_assign_department`
--
ALTER TABLE `group_assign_department`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `help_topic`
--
ALTER TABLE `help_topic`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `kb_article`
--
ALTER TABLE `kb_article`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `kb_article_relationship`
--
ALTER TABLE `kb_article_relationship`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `kb_category`
--
ALTER TABLE `kb_category`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `kb_comment`
--
ALTER TABLE `kb_comment`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `kb_pages`
--
ALTER TABLE `kb_pages`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `kb_settings`
--
ALTER TABLE `kb_settings`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `languages`
--
ALTER TABLE `languages`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `log_notification`
--
ALTER TABLE `log_notification`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `mailbox_protocol`
--
ALTER TABLE `mailbox_protocol`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `organization`
--
ALTER TABLE `organization`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `plugins`
--
ALTER TABLE `plugins`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_alert_notice`
--
ALTER TABLE `settings_alert_notice`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_auto_response`
--
ALTER TABLE `settings_auto_response`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_company`
--
ALTER TABLE `settings_company`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_email`
--
ALTER TABLE `settings_email`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_ratings`
--
ALTER TABLE `settings_ratings`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_system`
--
ALTER TABLE `settings_system`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `settings_ticket`
--
ALTER TABLE `settings_ticket`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `sla_plan`
--
ALTER TABLE `sla_plan`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `teams`
--
ALTER TABLE `teams`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `team_assign_agent`
--
ALTER TABLE `team_assign_agent`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `template`
--
ALTER TABLE `template`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `tickets`
--
ALTER TABLE `tickets`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;
--
-- AUTO_INCREMENT for table `ticket_attachment`
--
ALTER TABLE `ticket_attachment`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `ticket_collaborator`
--
ALTER TABLE `ticket_collaborator`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `ticket_form_data`
--
ALTER TABLE `ticket_form_data`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `ticket_priority`
--
ALTER TABLE `ticket_priority`
  MODIFY `priority_id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `ticket_source`
--
ALTER TABLE `ticket_source`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `ticket_status`
--
ALTER TABLE `ticket_status`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `ticket_thread`
--
ALTER TABLE `ticket_thread`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `timezone`
--
ALTER TABLE `timezone`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `time_format`
--
ALTER TABLE `time_format`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `user_assign_organization`
--
ALTER TABLE `user_assign_organization`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `version_check`
--
ALTER TABLE `version_check`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `widgets`
--
ALTER TABLE `widgets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `canned_response`
--
ALTER TABLE `canned_response`
  ADD CONSTRAINT `canned_response_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `department`
--
ALTER TABLE `department`
  ADD CONSTRAINT `department_ibfk_1` FOREIGN KEY (`sla`) REFERENCES `sla_plan` (`id`) ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
