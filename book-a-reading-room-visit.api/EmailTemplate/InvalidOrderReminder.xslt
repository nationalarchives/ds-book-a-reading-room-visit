<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="Root">
		<body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
			<p>
				Dear <xsl:value-of select="Name" />,
			</p>
			<p>
				You have a provisional booking to visit The National Archives’ reading rooms. This is a polite reminder that you need to complete your document order in order to confirm your visit.
			</p>
			<p>
				Make sure you complete the document order form by <b>
					<xsl:value-of select="CompleteByDate" /> at 23:59, local UK time.
				</b> If you do not complete the document order form by this deadline your visit will be automatically cancelled.
			</p>

			<h3 style="margin-top: 2em;">Complete your document order:</h3>
			<p>
				You will need your booking reference and reader's ticket number to complete this step. Use this link: <xsl:value-of select="ReturnURL" />
			</p>

			<h3 style="margin-top: 2em;">Your booking summary:</h3>
			<p>
				Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
				Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" /><br/>
				Visit type: <xsl:value-of select="VisitType" /><br/>
				Visit date: <xsl:value-of select="VisitStartDate" /><br/>
				<xsl:if test="string(ReadingRoom)">
					Reading room: <xsl:value-of select="ReadingRoom" />
				</xsl:if>
			</p>
			<p>
				Use Discovery, our catalogue, to gather your booking references:
				<a href="http://discovery.nationalarchives.gov.uk/">
					http://discovery.nationalarchives.gov.uk/
				</a>
			</p>

			<h3 style="margin-top: 2em;">Cancel your visit:</h3>
			<p>
				You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
			</p>

			<p>
				This email inbox is not being monitored.
			</p>
		</body>
	</xsl:template>
</xsl:stylesheet>
