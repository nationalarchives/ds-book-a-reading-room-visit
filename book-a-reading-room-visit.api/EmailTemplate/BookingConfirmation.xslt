<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        Your document order for The National Archives’ reading rooms has been submitted, and we will begin preparing your order for your visit on <xsl:value-of select="VisitStartDate" />.
      </p>
      <p>
        Please note that this is a confirmation of your booking, not the availability of the documents you have requested.
        While we do our very best to make all documents requested available, there are some instances where this is not possible.
        If more than half of your document requests become unavailable we will contact you the working day before your visit.
      </p>
      <p>
        During your visit, you will be able to order additional documents to view (an additional three documents at a time).
      </p>
      <h3 style="margin-top: 2em;">Your booking summary </h3>
      <p>
        Name: <xsl:value-of select="Name" /><br/>
        Telephone number is: <xsl:value-of select="Phone" /><br/>
        Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
        Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" /><br/>
        Access needs and research requests:<xsl:value-of select="AdditionalRequirements" />
      </p>
      <table style="text-align: left;border: 1px solid #999;border-collapse: collapse;margin: 2em 0;">
        <tbody>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit type</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitType" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit date</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <xsl:if test="string(ReadingRoom)">
            <tr>
              <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Reading room</th>
              <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
                <xsl:value-of select="ReadingRoom" />
              </td>
            </tr>
          </xsl:if>
        </tbody>
      </table>

      <h3 style="margin-top: 2em;">Your document order</h3>

      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <hr />

      <h3 style="margin-top: 2em;">Prepare for your visit</h3>
      <p>
        Bring your reader’s ticket. If you have a temporary ticket number, remember to bring the correct documents to complete your registration. 
        See a list of acceptable forms of identification: <a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/do-i-need-a-readers-ticket/">https://www.nationalarchives.gov.uk/about/visit-us/researching-here/do-i-need-a-readers-ticket/</a>
      </p>
      <p style="font-weight: bold;">
        Check these links before your visit:
      </p>
      <ul style="list-style: none;">
        <li>
          See our opening times: <a href="https://www.nationalarchives.gov.uk/about/visit-us/opening-times/ ">https://www.nationalarchives.gov.uk/about/visit-us/opening-times/</a>
        </li>
        <li>
          What to expect when you visit:<a href="https://www.nationalarchives.gov.uk/about/visit-us/researching-here/">https://www.nationalarchives.gov.uk/about/visit-us/researching-here/</a>
        </li>
        <li>
          Please note we are replacing the windows in the older part of our building until November 2025 and the work may be noisy.
          We apologise for any disruption to your visit <a target="_blank" href="https://www.nationalarchives.gov.uk/about/news/improvement-work-to-our-building-continues/">Learn more.</a>.
        </li>
        <li>
          From 29 April, mandatory bag checks will be introduced for all visitors upon entrance to the building.
          We recommend that you only bring what you need with you. Our visitor regulations including a full list of prohibited items can be found here: https://www.nationalarchives.gov.uk/about/visit-us/visitor-regulations/
        </li>
      </ul>

      <h3 style="margin-top: 2em;">Cancel your visit</h3>
      <p>
        You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
      </p>
      <h3 style="margin-top: 2em;">Complete our survey</h3>
      <p>
        This is a redesigned service. Complete our survey so we can keep improving this service:
        <a href="https://www.smartsurvey.co.uk/s/NewVisitBooking/">https://www.smartsurvey.co.uk/s/NewVisitBooking/</a>
      </p>
      <p>
        This email inbox is not being monitored.
      </p>
    </body>
  </xsl:template>
</xsl:stylesheet>
