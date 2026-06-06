START TRANSACTION;

SELECT ('Creating Categories...') AS Message;

INSERT INTO category (
	id,
	name,
	slug,
	parent_id,
	sort_order
) VALUES
(
	'019e7405-a74c-71f4-b788-1cd0827ddf94',
	'Light Controls',
	'light-controls',
	NULL,
	1
),
(
	'019e7405-a74d-73f2-9f8c-e1c56b500dfe',
	'Light Bulbs',
	'light-bulbs',
	NULL,
	2
),
(
	'019e7405-a74d-73f2-9f8c-e1c61c39c67e',
	'Wiring Devices and Light Controls',
	'wiring-devices-and-light-controls',
	NULL,
	3
),
(
	'019e7405-a74d-73f2-9f8c-e1c748c617cd',
	'Outlets',
	'outlets',
	'019e7405-a74d-73f2-9f8c-e1c61c39c67e',
	4
),
(
	'019e7405-a74d-73f2-9f8c-e1c811d785a8',
	'Protection Devices',
	'protection-devices',
	'019e7405-a74d-73f2-9f8c-e1c61c39c67e',
	5
),
(
	'019e7405-a74d-73f2-9f8c-e1c9676bdc35',
	'Light Switches',
	'light-switches',
	'019e7405-a74d-73f2-9f8c-e1c61c39c67e',
	6
),
(
	'019e7405-a74d-73f2-9f8c-e1ca29179513',
	'Cable',
	'cable',
	NULL,
	7
),
(
	'019e7405-a74d-73f2-9f8c-e1cb8282173a',
	'Fire Safety',
	'fire-safety',
	NULL,
	8
),
(
	'019e7405-a74d-73f2-9f8c-e1cccf0bce6d',
	'Electrical Tool',
	'electrical-tool',
	NULL,
	9
),
(
	'019e7405-a74d-73f2-9f8c-e1cd0fb2091b',
	'Electrical Testers',
	'electrical-testers',
	'019e7405-a74d-73f2-9f8c-e1cccf0bce6d',
	10
),
(
	'019e7405-a74d-73f2-9f8c-e1ce761e04d9',
	'Wire Connectors',
	'wire-connectors',
	'019e7405-a74d-73f2-9f8c-e1cccf0bce6d',
	11
);

COMMIT;
